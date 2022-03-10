using System;
using System.Reflection;
using System.Reflection.Emit;

namespace FireMapper
{
    public class DynamicSimplePropertyBinderBuilder
    {
        private readonly AssemblyBuilder ab;
        private readonly ModuleBuilder mb;
        private readonly Type domain;
        private readonly AssemblyName aName;

        public DynamicSimplePropertyBinderBuilder(Type domain)
        {
            this.domain = domain;
            aName = new AssemblyName(domain.Name + "SimplePropertyBinder");
            ab = AssemblyBuilder.DefineDynamicAssembly(
                aName,
                AssemblyBuilderAccess.RunAndSave);

            // For a single-module assembly, the module name is usually
            // the assembly name plus an extension.
            mb = ab.DefineDynamicModule(aName.Name, aName.Name + ".dll");
        }

        public void SaveModule()
        {
            ab.Save(aName.Name + ".dll");
        }

        public Type GeneratePropertyBinderFor(MemberInfo m)
        {
            if (m.MemberType == MemberTypes.Property)
                return GeneratePropertyBinderFor(m as PropertyInfo);
            else
                throw new InvalidOperationException("There is no dynamic property binder support for member of type " + m.MemberType);
        }

        public Type GeneratePropertyBinderFor(PropertyInfo p)
        {
            // define type
            TypeBuilder propertyBinderType = mb.DefineType(
                domain.Name + p.Name + "SimplePropertyBinder", TypeAttributes.Public, typeof(AbstractSimplePropertyBinder));

            // define constructor
            BuildConstructor(propertyBinderType, p);

            // define method GetValue
            BuildGetValue(propertyBinderType, p);

            // finish type
            return propertyBinderType.CreateType();
        }

        private void BuildConstructor(TypeBuilder propertyBinderType, PropertyInfo p)
        {
            Type[] parameterTypes = { typeof(Type), typeof(bool) };
            ConstructorBuilder ctor = propertyBinderType.DefineConstructor(
                MethodAttributes.Public,
                CallingConventions.Standard,
                parameterTypes); // Type.EmptyTypes

            ILGenerator il = ctor.GetILGenerator();
            il.Emit(OpCodes.Ldarg_0);                   // load this
            il.Emit(OpCodes.Ldarg_1);                   // load Type => il.Emit(OpCodes.Ldtoken, p.PropertyType);
            il.Emit(OpCodes.Ldstr, p.Name);             // load property.Name
            il.Emit(OpCodes.Ldarg_2);                   // load IsFireIgnore => il.Emit(IsFireIgnore ? OpCodes.Ldc_I4_1 : OpCodes.Ldc_I4_0);
            il.Emit(                                    // call AbstractSimplePropertyBinder::.ctor(Type, string, bool)
                OpCodes.Call,
                typeof(AbstractSimplePropertyBinder).GetConstructor(new Type[] { typeof(Type), typeof(string), typeof(bool) }));
            il.Emit(OpCodes.Ret);                       // return
        }

        private void BuildGetValue(TypeBuilder propertyBinderType, PropertyInfo p)
        {
            MethodBuilder methodBuilder = propertyBinderType.DefineMethod(
                "GetValue",
                MethodAttributes.Public | MethodAttributes.Virtual | MethodAttributes.HideBySig,
                typeof(object),
                new Type[] { typeof(object) });

            ILGenerator il = methodBuilder.GetILGenerator();
            LocalBuilder V_0 = il.DeclareLocal(domain);
            il.Emit(OpCodes.Ldarg_1);                   // load object target
            if(domain.IsValueType)
            {
                il.Emit(OpCodes.Unbox_Any, domain);     // caststruct object to domain
                il.Emit(OpCodes.Stloc, V_0);
                il.Emit(OpCodes.Ldloca_S, V_0);
                il.Emit(
                    OpCodes.Call,
                    p.GetGetMethod());                  // domain.GetMethod($"get_{p.Name}")
            }
            else
            {
                il.Emit(OpCodes.Castclass, domain);     // castclass object to domain
                il.Emit(OpCodes.Stloc, V_0);
                il.Emit(OpCodes.Ldloc, V_0);
                il.Emit(
                    OpCodes.Callvirt,
                    p.GetGetMethod());                  // domain.GetMethod($"get_{p.Name}")
            }
            if (p.PropertyType.IsPrimitive || p.PropertyType.IsValueType)
            {
                il.Emit(OpCodes.Box, p.PropertyType);   // box
            }
            il.Emit(OpCodes.Ret);                       // return
        }
    }
}
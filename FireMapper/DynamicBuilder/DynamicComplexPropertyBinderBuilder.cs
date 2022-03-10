using System;
using System.Reflection;
using System.Reflection.Emit;

namespace FireMapper
{
    public class DynamicComplexPropertyBinderBuilder
    {
        private readonly AssemblyBuilder ab;
        private readonly ModuleBuilder mb;
        private readonly Type domain;
        private readonly AssemblyName aName;

        public DynamicComplexPropertyBinderBuilder(Type domain)
        {
            this.domain = domain;
            aName = new AssemblyName(domain.Name + "ComplexPropertyBinder");
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

        public Type GeneratePropertyBinderFor(PropertyInfo p, PropertyInfo FireKeyProperty)
        {
            // define type
            TypeBuilder propertyBinderType = mb.DefineType(
                domain.Name + p.Name + "ComplexPropertyBinder", TypeAttributes.Public, typeof(AbstractComplexPropertyBinder));

            // define constructor
            BuildConstructor(propertyBinderType, p);

            // define method GetValue
            BuildGetValue(propertyBinderType, p);

            // define method GetFireKeyValue
            BuildGetFireKeyValue(propertyBinderType, p, FireKeyProperty);

            // finish type
            return propertyBinderType.CreateType();
        }

        private void BuildConstructor(TypeBuilder propertyBinderType, PropertyInfo p)
        {
            Type[] parameterTypes = { typeof(Type), typeof(bool), typeof(IDataMapper) };
            ConstructorBuilder ctor = propertyBinderType.DefineConstructor(
                MethodAttributes.Public,
                CallingConventions.Standard,
                parameterTypes);

            ILGenerator il = ctor.GetILGenerator();
            il.Emit(OpCodes.Ldarg_0);                   // load this
            il.Emit(OpCodes.Ldarg_1);                   // load Type
            il.Emit(OpCodes.Ldstr, p.Name);             // property.Name
            il.Emit(OpCodes.Ldarg_2);                   // load IsFireIgnore
            il.Emit(OpCodes.Ldarg_3);                   // load DataMapper
            il.Emit(                                    // call AbstractComplexPropertyBinder::.ctor(Type, string, bool, IDataMapper)
                OpCodes.Call,
                typeof(AbstractComplexPropertyBinder).GetConstructor(new Type[] { typeof(Type), typeof(string), typeof(bool), typeof(IDataMapper) }));
            il.Emit(OpCodes.Ret);
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
            if (domain.IsValueType)
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

        private void BuildGetFireKeyValue(TypeBuilder propertyBinderType, PropertyInfo p, PropertyInfo FireKeyProperty)
        {
            MethodBuilder methodBuilder = propertyBinderType.DefineMethod(
                "GetFireKeyValue",
                MethodAttributes.Public | MethodAttributes.Virtual | MethodAttributes.HideBySig,
                typeof(object),
                new Type[] { typeof(object) });

            ILGenerator il = methodBuilder.GetILGenerator();
            LocalBuilder V_0 = il.DeclareLocal(p.PropertyType);
            il.Emit(OpCodes.Ldarg_1);                               // load object target
            if (domain.IsValueType)
            {
                il.Emit(OpCodes.Unbox_Any, p.PropertyType);         // caststruct object to p.PropertyType
                il.Emit(OpCodes.Stloc, V_0);
                il.Emit(OpCodes.Ldloca_S, V_0);
                il.Emit(
                    OpCodes.Call,
                    FireKeyProperty.GetGetMethod());                // p.PropertyType.GetMethod($"get_{FireKeyName}")
            } else
            {
                il.Emit(OpCodes.Castclass, p.PropertyType);         // castclass object to p.PropertyType
                il.Emit(OpCodes.Stloc, V_0);
                il.Emit(OpCodes.Ldloc, V_0);
                il.Emit(
                    OpCodes.Callvirt,
                    FireKeyProperty.GetGetMethod());                // p.PropertyType.GetMethod($"get_{FireKeyName}")
            }
            if (FireKeyProperty.PropertyType.IsPrimitive)
            {
                il.Emit(OpCodes.Box, FireKeyProperty.PropertyType); // box
            }
            il.Emit(OpCodes.Ret);                                   // return
        }
    }
}
using System;

namespace FireMapper.Test.Domain
{
    public class AnotherCourseNamePropertyBinder : AbstractSimplePropertyBinder
    {
        public AnotherCourseNamePropertyBinder(Type type, bool IsFireIgnore) : base(typeof(string), "Name", false) { }

        public override object GetValue(object target)
        {
            Course c = (Course)target;
            return c.Name;
        }

        public object GetDefaultValue_v2(Type target)
        {
            return target == typeof(string) ? "" : Activator.CreateInstance(target);
        }

        public object GetDefaultValue_v3(Type target)
        {
            return target == typeof(string) ? "" : default;
        }

    }
}

/*
{
  // Code size       23 (0x17)
  .maxstack  8
  IL_0000:  ldarg.0
  IL_0001:  ldtoken    [mscorlib]System.String
  IL_0006:  call       class [mscorlib]System.Type [mscorlib]System.Type::GetTypeFromHandle(valuetype [mscorlib]System.RuntimeTypeHandle)
  IL_000b:  ldstr      "Name"
  IL_0010:  ldc.i4.0
  IL_0011:  call       instance void [FireMapper]FireMapper.AbstractSimplePropertyBinder::.ctor(class [mscorlib]System.Type,
                                                                                                string,
                                                                                                bool)
  IL_0016:  ret
} // end of method AnotherCourseNamePropertyBinder::.ctor
*/

/*
.method public hidebysig virtual instance object 
        GetValue(object target) cil managed
{
  // Code size       14 (0xe)
  .maxstack  1
  .locals init (class FireMapper.Test.Domain.Course V_0)
  IL_0000:  ldarg.1
  IL_0001:  castclass  FireMapper.Test.Domain.Course
  IL_0006:  stloc.0
  IL_0007:  ldloc.0
  IL_0008:  callvirt   instance string FireMapper.Test.Domain.Course::get_Name()
  IL_000d:  ret
} // end of method CourseNamePropertyBinder::GetValue
*/

/*
.method public hidebysig instance object 
        GetDefaultValue_v2(class [mscorlib]System.Type target) cil managed
{
  // Code size       31 (0x1f)
  .maxstack  8
  IL_0000:  ldarg.1
  IL_0001:  ldtoken    [mscorlib]System.String
  IL_0006:  call       class [mscorlib]System.Type [mscorlib]System.Type::GetTypeFromHandle(valuetype [mscorlib]System.RuntimeTypeHandle)
  IL_000b:  call       bool [mscorlib]System.Type::op_Equality(class [mscorlib]System.Type,
                                                               class [mscorlib]System.Type)
  IL_0010:  brtrue.s   IL_0019
  IL_0012:  ldarg.1
  IL_0013:  call       object [mscorlib]System.Activator::CreateInstance(class [mscorlib]System.Type)
  IL_0018:  ret
  IL_0019:  ldstr      ""
  IL_001e:  ret
} // end of method AnotherCourseNamePropertyBinder::GetDefaultValue_v2
 */

/*
.method public hidebysig instance object 
        GetDefaultValue_v3(class [mscorlib]System.Type target) cil managed
{
  // Code size       26 (0x1a)
  .maxstack  8
  IL_0000:  ldarg.1
  IL_0001:  ldtoken    [mscorlib]System.String
  IL_0006:  call       class [mscorlib]System.Type [mscorlib]System.Type::GetTypeFromHandle(valuetype [mscorlib]System.RuntimeTypeHandle)
  IL_000b:  call       bool [mscorlib]System.Type::op_Equality(class [mscorlib]System.Type,
                                                               class [mscorlib]System.Type)
  IL_0010:  brtrue.s   IL_0014
  IL_0012:  ldnull
  IL_0013:  ret
  IL_0014:  ldstr      ""
  IL_0019:  ret
} // end of method AnotherCourseNamePropertyBinder::GetDefaultValue_v3
 */
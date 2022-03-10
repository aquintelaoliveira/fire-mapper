using System;

namespace FireMapper.Test.Domain
{
    public class CourseNamePropertyBinder : AbstractSimplePropertyBinder
    {
        public CourseNamePropertyBinder(Type type, bool IsFireIgnore) : base(type, "Name", IsFireIgnore) { }

        public override object GetValue(object target)
        {
            Course c = (Course)target;
            return c.Name;
        }
    }
}

/*
.method public hidebysig specialname rtspecialname 
        instance void  .ctor(class [mscorlib]System.Type 'type',
                             bool IsFireIgnore) cil managed
{
  // Code size       14 (0xe)
  .maxstack  8
  IL_0000:  ldarg.0
  IL_0001:  ldarg.1
  IL_0002:  ldstr      "Name"
  IL_0007:  ldarg.2
  IL_0008:  call       instance void [FireMapper]FireMapper.AbstractSimplePropertyBinder::.ctor(class [mscorlib]System.Type,
                                                                                                string,
                                                                                                bool)
  IL_000d:  ret
} // end of method CourseNamePropertyBinder::.ctor
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
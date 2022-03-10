using System;

namespace FireMapper.Test.Domain
{
    public class CourseCourseTypePropertyBinder : AbstractComplexPropertyBinder
    {
        public CourseCourseTypePropertyBinder(Type type, bool IsFireIgnore, IDataMapper DataMapper) 
            : base(type, "CourseType", IsFireIgnore, DataMapper) { }

        public override object GetValue(object target)
        {
            Course c = (Course)target;
            return c.CourseType;
        }

        public override object GetFireKeyValue(object target)
        {
            Course c = (Course)target;
            return c.CourseType.Id;
        }
    }
}

/*
.method public hidebysig specialname rtspecialname 
        instance void  .ctor(class [mscorlib]System.Type 'type',
                             bool IsFireIgnore,
                             class [FireMapper]FireMapper.IDataMapper DataMapper) cil managed
{
  // Code size       15 (0xf)
  .maxstack  8
  IL_0000:  ldarg.0
  IL_0001:  ldarg.1
  IL_0002:  ldstr      "CourseType"
  IL_0007:  ldarg.2
  IL_0008:  ldarg.3
  IL_0009:  call       instance void [FireMapper]FireMapper.AbstractComplexPropertyBinder::.ctor(class [mscorlib]System.Type,
                                                                                                 string,
                                                                                                 bool,
                                                                                                 class [FireMapper]FireMapper.IDataMapper)
  IL_000e:  ret
} // end of method CourseCourseTypePropertyBinder::.ctor
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
  IL_0008:  callvirt   instance class FireMapper.Test.Domain.CourseType FireMapper.Test.Domain.Course::get_CourseType()
  IL_000d:  ret
} // end of method CourseCourseTypePropertyBinder::GetValue
*/

/*
.method public hidebysig virtual instance object 
        GetFireKeyValue(object target) cil managed
{
  // Code size       19 (0x13)
  .maxstack  1
  .locals init (class FireMapper.Test.Domain.Course V_0)
  IL_0000:  ldarg.1
  IL_0001:  castclass  FireMapper.Test.Domain.Course
  IL_0006:  stloc.0
  IL_0007:  ldloc.0
  IL_0008:  callvirt   instance class FireMapper.Test.Domain.CourseType FireMapper.Test.Domain.Course::get_CourseType()
  IL_000d:  callvirt   instance string FireMapper.Test.Domain.CourseType::get_Id()
  IL_0012:  ret
} // end of method CourseCourseTypePropertyBinder::GetFireKeyValue
 */
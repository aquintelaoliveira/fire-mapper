using System;

namespace FireMapper.Test.Domain
{
    public class StudentStructClassroomPropertyBinder : AbstractSimplePropertyBinder
    {
        public StudentStructClassroomPropertyBinder(Type type, bool IsFireIgnore) : base(type, "Name", IsFireIgnore) { }

        public override object GetValue(object target)
        {
            StudentStruct ss = (StudentStruct)target;
            return ss.Name;
        }

        public object GetValue_2(object target)
        {
            StudentStruct ss = (StudentStruct)target;
            return ss.Classroom;
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

/*
.method public hidebysig virtual instance object 
        GetValue(object target) cil managed
{
  // Code size       15 (0xf)
  .maxstack  1
  .locals init (valuetype FireMapper.Test.Domain.StudentStruct V_0)
  IL_0000:  ldarg.1
  IL_0001:  unbox.any  FireMapper.Test.Domain.StudentStruct
  IL_0006:  stloc.0
  IL_0007:  ldloca.s   V_0
  IL_0009:  call       instance string FireMapper.Test.Domain.StudentStruct::get_Name()
  IL_000e:  ret
} // end of method StudentStructClassroomPropertyBinder::GetValue
*/

/*
.method public hidebysig instance object 
        GetValue_2(object target) cil managed
{
  // Code size       20 (0x14)
  .maxstack  1
  .locals init (valuetype FireMapper.Test.Domain.StudentStruct V_0)
  IL_0000:  ldarg.1
  IL_0001:  unbox.any  FireMapper.Test.Domain.StudentStruct
  IL_0006:  stloc.0
  IL_0007:  ldloca.s   V_0
  IL_0009:  call       instance valuetype FireMapper.Test.Domain.ClassroomStruct FireMapper.Test.Domain.StudentStruct::get_Classroom()
  IL_000e:  box        FireMapper.Test.Domain.ClassroomStruct
  IL_0013:  ret
} // end of method StudentStructClassroomPropertyBinder::GetValue_2
*/
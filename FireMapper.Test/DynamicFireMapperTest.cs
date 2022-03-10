using System;
using Xunit;
using Xunit.Abstractions;

using FireMapper.Test.Domain;
using System.Reflection;
using System.Collections.Generic;

namespace FireMapper.Test
{
    [Collection("FireMapperFixture collection")]
    public class DynamicFireMapperTest
    {
        readonly ITestOutputHelper output;
        private readonly FireMapperFixture fix;
        DynamicFireMapper course_wds; // Course DynamicFireMapper with WeakDataSource
        DynamicFireMapper course_struct_wds; // CourseStruct DynamicFireMapper with WeakDataSource

        public DynamicFireMapperTest(ITestOutputHelper output, FireMapperFixture fix)
        {
            this.output = output;
            this.fix = fix;
            this.course_wds = new DynamicFireMapper(typeof(Course), fix.WeakDataSourceFactory);
            this.course_struct_wds = new DynamicFireMapper(typeof(CourseStruct), fix.WeakDataSourceFactory);
        }

        [Fact]
        public void TestDynamicSimplePropertyBinderForCourseName_GetValue()
        {
            PropertyInfo CourseNameproperty = typeof(Course).GetProperty("Name");

            DynamicSimplePropertyBinderBuilder builder = new DynamicSimplePropertyBinderBuilder(typeof(Course));
            Type simplePropertyBinderType = builder.GeneratePropertyBinderFor(CourseNameproperty);
            object[] args = { typeof(string), false };
            IPropertyBinder propertyBinder = (IPropertyBinder)Activator.CreateInstance(simplePropertyBinderType, args);

            ///
            /// Get Course By Id
            /// 
            Course course = (Course)course_wds.GetById("fixture-course-1-id");
            Assert.Equal(course.Name, propertyBinder.GetValue(course));
        }

        [Fact]
        public void TestDynamicSimplePropertyBinderForCoursePrice_GetValue()
        {
            PropertyInfo CoursePriceProperty = typeof(Course).GetProperty("Price");

            DynamicSimplePropertyBinderBuilder builder = new DynamicSimplePropertyBinderBuilder(typeof(Course));
            Type simplePropertyBinderType = builder.GeneratePropertyBinderFor(CoursePriceProperty);
            object[] args = { typeof(string), true };
            IPropertyBinder propertyBinder = (IPropertyBinder)Activator.CreateInstance(simplePropertyBinderType, args);

            ///
            /// Get Course By Id
            /// 
            Course course = (Course)course_wds.GetById("fixture-course-1-id");
            Assert.Equal(course.Price, propertyBinder.GetValue(course));
        }

        [Fact]
        public void TestDynamicComplexPropertyBinderForCourseCourseType_GetValue()
        {
            PropertyInfo CourseTypeProperty = typeof(Course).GetProperty("CourseType");
            PropertyInfo FireKeyProperty = typeof(CourseType).GetProperty("Id");

            DynamicComplexPropertyBinderBuilder builder = new DynamicComplexPropertyBinderBuilder(typeof(Course));
            Type complexPropertyBinderType = builder.GeneratePropertyBinderFor(CourseTypeProperty, FireKeyProperty);
            // builder.SaveModule();
            object[] args = { typeof(CourseType), false, new DynamicFireMapper(typeof(CourseType), fix.WeakDataSourceFactory) };
            AbstractComplexPropertyBinder propertyBinder = (AbstractComplexPropertyBinder)Activator.CreateInstance(complexPropertyBinderType, args);

            ///
            /// Get Course By Id
            /// 
            Course course = (Course)course_wds.GetById("fixture-course-1-id");
            Assert.Equal(course.CourseType.Type, ((CourseType)propertyBinder.GetValue(course)).Type);
        }

        [Fact]
        public void TestDynamicComplexPropertyBinderForCourseCourseType_GetFireKeyValue()
        {
            PropertyInfo CourseCourseTypeProperty = typeof(Course).GetProperty("CourseType");
            PropertyInfo FireKeyProperty = typeof(CourseType).GetProperty("Id");

            DynamicComplexPropertyBinderBuilder builder = new DynamicComplexPropertyBinderBuilder(typeof(Course));
            Type complexPropertyBinderType = builder.GeneratePropertyBinderFor(CourseCourseTypeProperty, FireKeyProperty);
            object[] args = { typeof(CourseType), false, new DynamicFireMapper(typeof(CourseType), fix.WeakDataSourceFactory) };
            AbstractComplexPropertyBinder propertyBinder = (AbstractComplexPropertyBinder)Activator.CreateInstance(complexPropertyBinderType, args);

            ///
            /// Get Course By Id
            /// 
            Course course = (Course)course_wds.GetById("fixture-course-1-id");
            Assert.Equal(course.CourseType.Id, propertyBinder.GetFireKeyValue(course.CourseType));
        }

        [Fact]
        public void TestDynamicSimplePropertyBinderForTestStudentStructName_GetValue()
        {
            PropertyInfo StudentStructNameProperty = typeof(StudentStruct).GetProperty("Name");

            DynamicSimplePropertyBinderBuilder builder = new DynamicSimplePropertyBinderBuilder(typeof(StudentStruct));
            Type simplePropertyBinderType = builder.GeneratePropertyBinderFor(StudentStructNameProperty);
            object[] args = { typeof(string), false };
            IPropertyBinder propertyBinder = (IPropertyBinder)Activator.CreateInstance(simplePropertyBinderType, args);

            ///
            /// Create StudentScruct
            /// 
            StudentStruct ss = new StudentStruct("1", "Student Struct 1", new ClassroomStruct("token-1", "Teacher 1"));
            Assert.Equal(ss.Name, propertyBinder.GetValue(ss));
        }

        [Fact]
        public void TestDynamicComplexPropertyBinderForTestStudentStructClassroom_GetValue()
        {
            PropertyInfo StudentStructClassroomProperty = typeof(StudentStruct).GetProperty("Classroom");
            PropertyInfo StructClassroomFireKeyProperty = typeof(ClassroomStruct).GetProperty("Token");

            DynamicComplexPropertyBinderBuilder builder = new DynamicComplexPropertyBinderBuilder(typeof(StudentStruct));
            Type complexPropertyBinderType = builder.GeneratePropertyBinderFor(StudentStructClassroomProperty, StructClassroomFireKeyProperty);
            object[] args = { typeof(ClassroomStruct), false, new DynamicFireMapper(typeof(ClassroomStruct), fix.WeakDataSourceFactory) };
            AbstractComplexPropertyBinder propertyBinder = (AbstractComplexPropertyBinder)Activator.CreateInstance(complexPropertyBinderType, args);

            ///
            /// Create StudentScruct
            /// 
            StudentStruct ss = new StudentStruct("1", "Student Struct 1", new ClassroomStruct("token-1", "Teacher 1"));
            Assert.Equal(ss.Classroom.Teacher, ((ClassroomStruct)propertyBinder.GetValue(ss)).Teacher);
        }

        [Fact]
        public void TestDynamicComplexPropertyBinderForTestStudentStructClassroom_GetFireKeyValue()
        {
            PropertyInfo StudentStructClassroomProperty = typeof(StudentStruct).GetProperty("Classroom");
            PropertyInfo StructClassroomFireKeyProperty = typeof(ClassroomStruct).GetProperty("Token");

            DynamicComplexPropertyBinderBuilder builder = new DynamicComplexPropertyBinderBuilder(typeof(StudentStruct));
            Type complexPropertyBinderType = builder.GeneratePropertyBinderFor(StudentStructClassroomProperty, StructClassroomFireKeyProperty);
            object[] args = { typeof(ClassroomStruct), false, new DynamicFireMapper(typeof(ClassroomStruct), fix.WeakDataSourceFactory) };
            AbstractComplexPropertyBinder propertyBinder = (AbstractComplexPropertyBinder)Activator.CreateInstance(complexPropertyBinderType, args);

            ///
            /// Create StudentScruct
            /// 
            StudentStruct ss = new StudentStruct("1", "Student Struct 1", new ClassroomStruct("token-1", "Teacher 1"));
            Assert.Equal(ss.Classroom.Token, propertyBinder.GetFireKeyValue(ss.Classroom));
        }

        #region DynamicFireMapper With WeakDataSource
        [Fact]
        public void GetAllFromWeakDataSource()
        {
            ///
            /// Get Course All Courses
            /// 
            int wdm_count = 0;
            foreach (var d in course_wds.GetAll())
            {
                output.WriteLine(d.ToString());
                wdm_count++;
            }
            Assert.Equal(3, wdm_count);
        }

        [Fact]
        public void GetByIdFromWeakDataSource()
        {
            ///
            /// Get Course By Id
            /// 
            Course course = (Course)course_wds.GetById("fixture-course-1-id");
            Assert.Equal("Course 1", course.Name);
            Assert.Equal("Type 1", course.CourseType.Type);
            Assert.Equal("Language 1", course.CourseType.ProgrammingLanguage.Language);
            Assert.Equal("Instructor 1", course.Instructor.Name);
        }

        [Fact]
        public void UpdateFromWeakDataSource()
        {
            ///
            /// Update Course
            /// 
            course_wds.Update(new Course(
                "fixture-course-2-id",
                "Updated Course 2",
                new CourseType(
                    "fixture-course-type-2-id",
                    "Type 2",
                    new ProgrammingLanguage(
                        "fixture-programming-language-2-id",
                        "Language 2"
                        )
                    ),
                "Course 2 Description",
                2.22,
                new Instructor(
                    "fixture-instructor-2-id",
                    "Instructor 2",
                    "instructor2@email.com"
                    )
                )
            );
            ///
            /// Get Course By Id
            /// 
            Course course = (Course)course_wds.GetById("fixture-course-2-id");
            Assert.Equal("Updated Course 2", course.Name);
        }

        [Fact]
        public void AddGetAndDeleteAndGetAgainFromWeakDataSource()
        {
            ///
            /// Add Course
            /// 
            course_wds.Add(new Course(
                "course-to-be-deleted-id",
                "Course To Be Deleted",
                new CourseType(
                    "course-type-to-be-deleted-id",
                    "Course Type To Be Deleted",
                    new ProgrammingLanguage(
                        "programming-language-to-be-delete-id",
                        "Language To Be Deleted"
                        )
                    ),
                "Course Description To Be Deleted",
                1.99,
                new Instructor(
                    "instructor-to-be-deleted-id",
                    "Instructor To Be Deleted",
                    "instructor-to-be-deleted-@email.com"
                    )
                )
            );
            /// 
            ///  Get Added Course By Id
            ///
            Course course = (Course)course_wds.GetById("course-to-be-deleted-id");
            Assert.Equal("Course To Be Deleted", course.Name);
            Assert.Equal("Course Type To Be Deleted", course.CourseType.Type);
            Assert.Equal("Language To Be Deleted", course.CourseType.ProgrammingLanguage.Language);
            Assert.Equal("Instructor To Be Deleted", course.Instructor.Name);
            ///
            /// Delete Course
            /// 
            course_wds.Delete("course-to-be-deleted-id");
            ///
            /// Get Deleted Course By Id
            /// 
            var exception = Assert.Throws<KeyNotFoundException>(
                () => course_wds.GetById(course.Id));
            Assert.Equal($"There is no entry in database for key {course.Id}!", exception.Message);
        }
        #endregion


        #region DynamicFireMapper With WeakDataSource with structs
        [Fact]
        public void GetByIdFromWeakDataSourceWithStructs()
        {
            ///
            /// Get Course By Id
            /// 
            CourseStruct course = (CourseStruct)course_struct_wds.GetById("fixture-course-1-id");
            Assert.Equal("Course 1", course.Name);
            Assert.Equal("Type 1", course.CourseType.Type);
            Assert.Equal("Language 1", course.CourseType.ProgrammingLanguage.Language);
            Assert.Equal("Instructor 1", course.Instructor.Name);
        }
        [Fact]
        public void AddUpdateGetAndDeleteAndGetAgainFromWeakDataSourceWithStructs()
        {
            ///
            /// Add CourseStruct
            /// 
            course_struct_wds.Add(new CourseStruct(
                "course-to-be-deleted-id",
                "Course To Be Deleted",
                new CourseTypeStruct(
                    "course-type-to-be-deleted-id",
                    "Course Type To Be Deleted",
                    new ProgrammingLanguageStruct(
                        "programming-language-to-be-delete-id",
                        "Language To Be Deleted"
                        )
                    ),
                "Course Description To Be Deleted",
                1.99,
                new InstructorStruct(
                    "instructor-to-be-deleted-id",
                    "Instructor To Be Deleted",
                    "instructor-to-be-deleted-@email.com"
                    )
                )
            );
            ///
            /// Update CourseStruct
            /// 
            course_struct_wds.Update(new CourseStruct(
                "course-to-be-deleted-id",
                "Course To Be Deleted - Updated",
                new CourseTypeStruct(
                    "course-type-to-be-deleted-id",
                    "Course Type To Be Deleted",
                    new ProgrammingLanguageStruct(
                        "programming-language-to-be-delete-id",
                        "Language To Be Deleted"
                        )
                    ),
                "Course Description To Be Deleted",
                1.99,
                new InstructorStruct(
                    "instructor-to-be-deleted-id",
                    "Instructor To Be Deleted",
                    "instructor-to-be-deleted-@email.com"
                    )
                )
            );
            /// 
            ///  Get Added CourseStruct By Id
            ///
            CourseStruct course = (CourseStruct)course_struct_wds.GetById("course-to-be-deleted-id");
            Assert.Equal("Course To Be Deleted - Updated", course.Name);
            Assert.Equal("Course Type To Be Deleted", course.CourseType.Type);
            Assert.Equal("Language To Be Deleted", course.CourseType.ProgrammingLanguage.Language);
            Assert.Equal("Instructor To Be Deleted", course.Instructor.Name);
            ///
            /// Delete CourseStruct
            /// 
            course_struct_wds.Delete("course-to-be-deleted-id");
            ///
            /// Get Deleted CourseStruct By Id
            /// 
            var exception = Assert.Throws<KeyNotFoundException>(
                () => course_struct_wds.GetById(course.Id));
            Assert.Equal($"There is no entry in database for key {course.Id}!", exception.Message);
        }
        #endregion

    }
}

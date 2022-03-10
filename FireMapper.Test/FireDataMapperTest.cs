using System;
using Xunit;
using Xunit.Abstractions;

using FireMapper.Test.Domain;
using System.Collections.Generic;

namespace FireMapper.Test
{
    [Collection("FireMapperFixture collection")]
    public class FireMapperTest
    {
        readonly ITestOutputHelper output;
        private readonly FireMapperFixture fix;
        FireDataMapper course_fds;  // Course DataFireMapper with FireDataSource
        FireDataMapper course_wds;  // Course DataFireMapper with WeakDataSource

        public FireMapperTest(ITestOutputHelper output, FireMapperFixture fix)
        {
            this.output = output;
            this.fix = fix;
            this.course_fds = new FireDataMapper(typeof(Course), fix.FireDataSourceFactory);
            this.course_wds = new FireDataMapper(typeof(Course), fix.WeakDataSourceFactory);
        }

        [Fact]
        public void MissingFireCollectionAttribute()
        {
            var exception = Assert.Throws<InvalidOperationException>(
                () => new FireDataMapper(typeof(DummyClassWithoutFireCollectionAttribute), fix.FireDataSourceFactory));
            Assert.Equal("DummyClassWithoutFireCollectionAttribute is not a FireCollection!", exception.Message);
        }

        [Fact]
        public void MissingFireKeyAttribute()
        {
            var exception = Assert.Throws<InvalidOperationException>(
                () => new FireDataMapper(typeof(DummyClassWithoutFireKeyAttribute), fix.FireDataSourceFactory));
            Assert.Equal("DummyClassWithoutFireKeyAttribute is missing a FireKey!", exception.Message);
        }

        #region DataFireMapper With FireDataSource
        [Fact]
        public void GetAllFromFireDataSource()
        {
            ///
            /// Get Course All Courses
            /// 
            int fdm_count = 0;
            foreach (var d in course_fds.GetAll())
            {
                output.WriteLine(d.ToString());
                fdm_count++;
            }
            Assert.Equal(3, fdm_count);
        }

        [Fact]
        public void GetByIdFromFireDataSource()
        {
            ///
            /// Get Course By Id
            /// 
            Course course = (Course)course_fds.GetById("fixture-course-1-id");
            Assert.Equal("Course 1", course.Name);
            Assert.Equal("Type 1", course.CourseType.Type);
            Assert.Equal("Language 1", course.CourseType.ProgrammingLanguage.Language);
            Assert.Equal("Instructor 1", course.Instructor.Name);
        }

        [Fact]
        public void UpdateFromFireDataSource()
        {
            ///
            /// Update Course
            /// 
            course_fds.Update(new Course(
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
            Course course = (Course)course_fds.GetById("fixture-course-2-id");
            Assert.Equal("Updated Course 2", course.Name);
        }

        [Fact]
        public void AddGetAndDeleteAndGetAgainFromFireDataSource()
        {
            ///
            /// Add Course
            /// 
            course_fds.Add(new Course(
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
            /// Get Added Course By Id
            /// 
            Course course = (Course)course_fds.GetById("course-to-be-deleted-id");
            Assert.Equal("Course To Be Deleted", course.Name);
            Assert.Equal("Course Type To Be Deleted", course.CourseType.Type);
            Assert.Equal("Language To Be Deleted", course.CourseType.ProgrammingLanguage.Language);
            Assert.Equal("Instructor To Be Deleted", course.Instructor.Name);
            ///
            /// Delete Course
            /// 
            course_fds.Delete("course-to-be-deleted-id");
            ///
            /// Get Deleted Course By Id
            /// 
            Assert.Null(course_fds.GetById("course-to-be-deleted-id"));
        }
        #endregion

        #region DataFireMapper With WeakDataSource
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
    }
}

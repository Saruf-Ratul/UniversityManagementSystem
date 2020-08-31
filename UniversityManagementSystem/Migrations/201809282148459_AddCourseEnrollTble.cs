namespace UniversityManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCourseEnrollTble : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CourseEnrolls",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StudentId = c.Int(nullable: false),
                        CourseId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.CourseId)
                .ForeignKey("dbo.Students", t => t.StudentId)
                .Index(t => t.CourseId)
                .Index(t => t.StudentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CourseEnrolls", "StudentId", "dbo.Students");
            DropForeignKey("dbo.CourseEnrolls", "CourseId", "dbo.Courses");
            DropIndex("dbo.CourseEnrolls", new[] { "StudentId" });
            DropIndex("dbo.CourseEnrolls", new[] { "CourseId" });
            DropTable("dbo.CourseEnrolls");
        }
    }
}

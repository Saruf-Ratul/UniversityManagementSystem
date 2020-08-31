namespace UniversityManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddResultTbl : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StudentResults",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StudentId = c.Int(nullable: false),
                        CourseId = c.Int(nullable: false),
                        GradeId = c.DateTime(nullable: false),
                        Grade_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.CourseId)
                .ForeignKey("dbo.Grades", t => t.Grade_Id)
                .ForeignKey("dbo.Students", t => t.StudentId)
                .Index(t => t.CourseId)
                .Index(t => t.Grade_Id)
                .Index(t => t.StudentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StudentResults", "StudentId", "dbo.Students");
            DropForeignKey("dbo.StudentResults", "Grade_Id", "dbo.Grades");
            DropForeignKey("dbo.StudentResults", "CourseId", "dbo.Courses");
            DropIndex("dbo.StudentResults", new[] { "StudentId" });
            DropIndex("dbo.StudentResults", new[] { "Grade_Id" });
            DropIndex("dbo.StudentResults", new[] { "CourseId" });
            DropTable("dbo.StudentResults");
        }
    }
}

namespace UniversityManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateStudentTbl : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.StudentResults", "Grade_Id", "dbo.Grades");
            DropIndex("dbo.StudentResults", new[] { "Grade_Id" });
            DropColumn("dbo.StudentResults", "GradeId");
            RenameColumn(table: "dbo.StudentResults", name: "Grade_Id", newName: "GradeId");
            AlterColumn("dbo.StudentResults", "GradeId", c => c.Int(nullable: false));
            AlterColumn("dbo.StudentResults", "GradeId", c => c.Int(nullable: false));
            CreateIndex("dbo.StudentResults", "GradeId");
            AddForeignKey("dbo.StudentResults", "GradeId", "dbo.Grades", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StudentResults", "GradeId", "dbo.Grades");
            DropIndex("dbo.StudentResults", new[] { "GradeId" });
            AlterColumn("dbo.StudentResults", "GradeId", c => c.Int());
            AlterColumn("dbo.StudentResults", "GradeId", c => c.DateTime(nullable: false));
            RenameColumn(table: "dbo.StudentResults", name: "GradeId", newName: "Grade_Id");
            AddColumn("dbo.StudentResults", "GradeId", c => c.DateTime(nullable: false));
            CreateIndex("dbo.StudentResults", "Grade_Id");
            AddForeignKey("dbo.StudentResults", "Grade_Id", "dbo.Grades", "Id");
        }
    }
}

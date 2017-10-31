using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IMS.Models;
using Dapper;
using System.IO;

namespace IMS.Repository
{
    public class SqlLiteProjectRepository : SqLiteBaseRepository, IProjectRepository
    {
        public bool DeleteProject(long id)
        {
            if (!File.Exists(DbFile)) return false;
            using (var cnn = SimpleDbConnection())
            {
                cnn.Open();
                int result = cnn.Execute("update project set disabled=1 where Id=@Id", new { Id = id });
                return result > 0;
            }
        }

        public Project GetProject(long id)
        {
            if (!File.Exists(DbFile)) return null;
            using (var cnn = SimpleDbConnection())
            {
                cnn.Open();
                var result = cnn.QueryFirstOrDefault<Project>("select * from project where Id=@Id", new { Id = id });
                return result;
            }
        }

        public bool ModifyProject(Project project)
        {
            if (!File.Exists(DbFile)) return false;
            using (var cnn = SimpleDbConnection())
            {
                cnn.Open();
                var result = cnn.Execute(@" Update project set 
                                            VillageName=@VillageName,
                                            SignSort=@SignSort,
                                            HouseHolderName=@HouseHolderName,
                                            HouseNumber=@HouseNumber,
                                            CreateTime=@CreateTime,
                                            CreateBy=@CreateBy,
                                            ModifyTime=getdate(),
                                            ModifyBy=@ModifyBy,
                                            Disabled=@Disabled 
                                            where 
                                            Id=@Id", project);
                return result > 0;
            }
        }

        public long SaveProject(Project project)
        {
            if (!File.Exists(DbFile)) return 0;
            using (var cnn = SimpleDbConnection())
            {
                cnn.Open();
                var result = cnn.Query<long>(@" insert into project (VillageName,SignSort,HouseHolderName,HouseNumber,CreateTime,CreateBy,ModifyTime,ModifyBy,Disabled) values 
                                            (@VillageName,@SignSort,@HouseHolderName,@HouseNumber,getdate(),@CreateBy,getdate(),@ModifyBy,0);
                                             select last_insert_rowid()", project).First();
                return result;
            }
        }

        public static void CreateDatabase()
        {
            
            using (var cnn = SimpleDbConnection())
            {
                cnn.Open();
                cnn.Execute(
                    @"create table  IF NOT EXISTS Project
              (
                 Id                                 integer primary key AUTOINCREMENT,
                 VillageName                        varchar(100) not null,
                 SignSort                           integer not null,
                 HouseHolderName                    varchar(100) not null,
                 HouseNumber                        varchar(100) not null,
                 CreateTime                         datetime not null,
                 CreateBy                           varchar(100) not null,
                 ModifyTime                         datetime not null,
                 ModifyBy                           varchar(100) not null,
                 Disabled                           integer not null
              )");
            }
        }
    }
}
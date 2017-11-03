using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IMS.Models;
using Dapper;
using System.IO;

namespace IMS.Repository
{
    public class SqlServerProjectRepository : SqlServerBaseRepository, IProjectRepository
    {
        public bool DeleteProject(long id)
        {
            using (var cnn = SimpleDbConnection())
            {
                cnn.Open();
                int result = cnn.Execute("update project set disabled=1 where Id=@Id", new { Id = id });
                return result > 0;
            }
        }

        public Project GetProject(long id)
        {

            using (var cnn = SimpleDbConnection())
            {
                cnn.Open();
                var result = cnn.QueryFirstOrDefault<Project>("select * from project where Id=@Id", new { Id = id });
                return result;
            }
        }

        public bool ModifyProject(Project project)
        {
            using (var cnn = SimpleDbConnection())
            {
                cnn.Open();

                cnn.Execute("update [dbo].[Project] set SignSort=SignSort+1 where SignSort>=@SignSort and Disabled=0", new { SignSort = project.SignSort });
                
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

            using (var cnn = SimpleDbConnection())
            {
                cnn.Open();

                int maxSort = cnn.ExecuteScalar<int>(@"select top 1 SignSort from [dbo].[Project] where Disabled=0 order by SignSort desc");

                project.SignSort = maxSort + 1;

                var result = cnn.Execute(@" insert into project (VillageName,SignSort,HouseHolderName,HouseNumber,CreateTime,CreateBy,ModifyTime,ModifyBy,Disabled) values 
                                            (@VillageName,@SignSort,@HouseHolderName,@HouseNumber,getdate(),@CreateBy,getdate(),@ModifyBy,0)", project);
                return result;
            }
        }

        public List<Project> GetProjectList()
        {
            using (var cnn = SimpleDbConnection())
            {
                cnn.Open();
                var result = cnn.Query<Project>("select * from project where disabled=0 order by createtime desc")
                    .AsList<Project>();
                return result;
            }
        }

    }
}
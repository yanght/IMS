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
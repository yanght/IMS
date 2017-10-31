using IMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IMS.Repository
{
    public interface IProjectRepository
    {
        Project GetProject(long id);
        long SaveProject(Project project);
        bool ModifyProject(Project project);
        bool DeleteProject(long id);
    }
}
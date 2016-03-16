using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.CodeDom.Compiler;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using $solutionname$.Shared.DomainObjects;
using System.Threading;
using DatabaseGeneratedOption = System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedAttribute;

namespace $defaultnamespace$.Data.Mapping
{
    internal partial class $originalsafeitemname$ : EntityTypeConfiguration<$nomapitemname$>
    {
        partial void InitializePartial()
        {
            //Ignore(x => x.ColumnName);
            //Ignore(x => x.ColumnName);
            //Ignore(x => x.ColumnName);
        }
    }
}

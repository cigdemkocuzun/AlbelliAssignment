using System;
using System.Collections.Generic;
using System.Text;

namespace Albelli.Domain.SeedWork
{
    public interface IAuditableBaseEntity
    {
        string CreatedBy { get; set; }
        DateTime CreatedOn { get; set; }
        string LastModifiedBy { get; set; }
        DateTime? LastModifiedOn { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities;

public class TrackedEntity
{
    public DateTime? CreationDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public Guid? CreatedBy { get; set; }
    public bool IsDeleted { get; set; }
    public Guid? ModifiedBy { get; set; }


}

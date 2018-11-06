using System;
using System.Collections.Generic;
using System.Text;

namespace Odin.Core.Interfaces
{
    public interface ITrackingUserChange
    {
        DateTime CreatedOn { get; set; }
        int CreatedBy { get; set; }
        DateTime ModifiedOn { get; set; }
        int ModifiedBy { get; set; }
    }
}

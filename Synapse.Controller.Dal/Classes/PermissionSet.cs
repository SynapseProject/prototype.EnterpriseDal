using System;

using Suplex.Security.AclModel;

namespace Synapse.Services.Controller.Dal
{

    public class PermissionSet
    {
        public int Id { get; set; }                     // Primary Key from Suplex Table
        public Guid GroupUId { get; set; }              // Unique Id Of Suplex Security Group
        public String GroupName { get; set; }           // Name of Suplex Security Group
        public FileSystemRight Rights { get; set; }     // Group Rights to the Container
        public RecordState State { get; set; }          // Current State of the Record
    }
}
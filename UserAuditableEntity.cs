using Penguin.Cms.Attributes;
using Penguin.Entities;
using Penguin.Persistence.Abstractions.Attributes.Control;
using Penguin.Persistence.Abstractions.Attributes.Rendering;
using System;

namespace Penguin.Cms.Entities
{
    /// <summary>
    /// Base class for any entity that should have all changes logged along with identifying information of the person that made those changes
    /// </summary>
    public abstract class UserAuditableEntity : AuditableEntity
    {
        #region Properties

        /// <summary>
        /// The Guid of the user that created the object
        /// </summary>
        [DontAllow(DisplayContexts.List | DisplayContexts.Edit)]
        [CustomRoute(DisplayContexts.Edit, "Render", "UserRecord")]
        [Display(Name = "Created By")]
        public Guid Creator { get; set; }

        /// <summary>
        /// The Guid of the user that deleted the object
        /// </summary>
        [DontAllow(DisplayContexts.Any)]
        [CustomRoute(DisplayContexts.Edit, "Render", "UserRecord")]
        [Display(Name = "Deleted By")]
        public Guid Deleter { get; set; }

        /// <summary>
        /// The IP address of the user that created the object
        /// </summary>
        [DontAllow(DisplayContexts.List | DisplayContexts.Edit)]
        [Display(Name = "Creating IP")]
        public string IPCreated { get; set; }

        /// <summary>
        /// The IP Address of the user that deleted the object
        /// </summary>
        [DontAllow(DisplayContexts.List | DisplayContexts.Edit)]
        [Display(Name = "Deleting IP")]
        public string IPDeleted { get; set; }

        /// <summary>
        /// The IP Address of the last user to make changes to the object
        /// </summary>
        [DontAllow(DisplayContexts.List | DisplayContexts.Edit)]
        [Display(Name = "Last Modifying IP")]
        public string IPModified { get; set; }

        /// <summary>
        /// The Guid of the last user to make changes to the object
        /// </summary>
        [DontAllow(DisplayContexts.List | DisplayContexts.Edit)]
        [CustomRoute(DisplayContexts.Edit, "Render", "UserRecord")]
        [Display(Name = "Last Modified By")]
        public Guid LastModifier { get; set; }

        #endregion Properties
    }
}
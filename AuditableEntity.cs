using Penguin.Entities.Abstractions;
using Penguin.Persistence.Abstractions.Attributes.Control;
using Penguin.Persistence.Abstractions.Attributes.Rendering;
using System;

namespace Penguin.Cms.Entities
{
    /// <summary>
    /// The base class for entities whos changes should be tracked and stored
    /// </summary>
    public abstract class AuditableEntity : Entity, IAuditableEntity
    {
        #region Properties

        /// <summary>
        /// An overridable value reporesenting if changes should be tracked. Should be set to false for things like passwords, errors, etc
        /// </summary>
        [DontAllow(DisplayContexts.Any)]
        public virtual bool AuditLogChanges => true;

        /// <summary>
        /// The date the entity was deleted. Also used to determine if the entity IS deleted
        /// </summary>
        [DontAllow(DisplayContexts.Any)]
        [Display(Name = "Date Deleted")]
        public DateTime? DateDeleted { get; set; }

        /// <summary>
        /// The date the entity was last modified
        /// </summary>
        [DontAllow(DisplayContexts.List | DisplayContexts.Edit)]
        [Display(Name = "Last Modified")]
        public DateTime? DateModified { get; set; }

        /// <summary>
        /// A quick way to check if the DateDeleted has a value
        /// </summary>
        [DontAllow(DisplayContexts.Any)]
        public bool IsDeleted => this.DateDeleted.HasValue;

        #endregion Properties
    }
}
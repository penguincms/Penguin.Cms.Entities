using Penguin.Entities.Abstractions;
using Penguin.Persistence.Abstractions;
using Penguin.Persistence.Abstractions.Attributes;
using Penguin.Persistence.Abstractions.Attributes.Control;
using Penguin.Persistence.Abstractions.Attributes.Rendering;
using Penguin.Persistence.Abstractions.Attributes.Validation;
using System;

namespace Penguin.Cms.Entities
{
    /// <summary>
    /// The base Penguin.Entity class containing all the information/Keys for the Penuin CMS
    /// </summary>
    [Entity(EntityType.Entity)]
    public abstract class Entity : KeyedObject, IEntity
    {
        #region Properties

        /// <summary>
        /// The external ID for this entity intended to be used to look it up by users
        /// </summary>
        [DontAllow(DisplayContexts.Any)]
        [StringLength(200)]
        [Display(Name = "External Id")]
        public virtual string ExternalId { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// A guid representing this entity
        /// </summary>
        [DontAllow(DisplayContexts.Edit | DisplayContexts.List)]
        [Display(Name = "Unique Id")]
        [Index(IsUnique = true)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1720:Identifier contains type name", Justification = "Named for importance and distinction")]
        public virtual Guid Guid { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Used to store/retrieve the full name of the type to the database
        /// </summary>
        [DontAllow(DisplayContexts.Any)]
        [Display(Name = "Object Type")]
        public virtual string TypeName
        {
            get
            {
                if (this._typeName == null || string.IsNullOrEmpty(this._typeName))
                {
                    this._typeName = this.GetType().Module.ScopeName == "EntityProxyModule" ? this.GetType().BaseType.FullName : this.GetType().FullName;
                }
                return this._typeName;
            }
            set => this._typeName = value;
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Default inequality comparer. Uses Guid
        /// </summary>
        /// <param name="obj1">The first object</param>
        /// <param name="obj2">The second object</param>
        /// <returns>Inequality</returns>
        public static bool operator !=(Entity obj1, Entity obj2)
        {
            if (obj1 is null ^ obj2 is null)
            {
                return true;
            }
            else if (obj1 is null && obj2 is null)
            {
                return false;
            }
            else
            {
                return obj1?.GetHashCode() != obj2?.GetHashCode();
            }
        }

        /// <summary>
        /// Default equality comparer. Uses Guid
        /// </summary>
        /// <param name="obj1">The first object</param>
        /// <param name="obj2">The second object</param>
        /// <returns>equality</returns>
        public static bool operator ==(Entity obj1, Entity obj2)
        {
            if (obj1 is null ^ obj2 is null)
            {
                return false;
            }
            else if (obj1 is null && obj2 is null)
            {
                return true;
            }
            else
            {
                return obj1?.GetHashCode() == obj2?.GetHashCode();
            }
        }

        /// <summary>
        /// Checks if the objects are equal using Guid
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj is null || !(obj is Entity))
            {
                return false;
            }
            else
            {
                return ReferenceEquals(this, obj) || (obj as Entity)?.GetHashCode() == this.GetHashCode();
            }
        }

        /// <summary>
        /// Returns the Guid HashCode
        /// </summary>
        /// <returns>the Guid Hashcode</returns>
        public override int GetHashCode() => this.Guid.GetHashCode();

        #endregion Methods

        /// <summary>
        /// The date the entity was first created
        /// </summary>
        [DontAllow(DisplayContexts.List | DisplayContexts.Edit)]
        [Display(Name = "Date Created")]
        public virtual DateTime DateCreated { get; set; } = DateTime.Now;

        [DontAllow(DisplayContexts.Any)]
        private string _typeName { get; set; }
    }
}
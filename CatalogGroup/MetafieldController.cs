using EPiServer.Cms.UI.AspNetIdentity;
using Foundation.Infrastructure.Cms.Users;
using Mediachase.BusinessFoundation.Data;
using Mediachase.BusinessFoundation.Data.Meta.Management;
using Mediachase.Commerce.Customers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Foundation.Custom
{
    [ApiController]
    [Route("CatalogGroup/Metafield")]
    public class MetafieldController : ControllerBase
    {
        [HttpGet]
        [Route("AddFieldOrganization")]
        public async Task<ActionResult<string>> AddFieldOrganization() 
        {
            return Ok(AddMetaFieldCheckboxBooleanOrganization());
        } 
        
        [HttpGet]
        [Route("AddFieldContact")]
        public async Task<ActionResult<string>> AddFieldContact() 
        {
            return Ok(AddMetaFieldCheckboxBooleanContact());
        }

        string AddMetaFieldCheckboxBooleanOrganization()
        {
            string name = "Test";
            string friendlyName = name;
            var typeName = MetaFieldType.CheckboxBoolean;
            var orgMetaClass = DataContext.Current.MetaModel.MetaClasses[OrganizationEntity.ClassName];

            var metaClass = orgMetaClass;

            var existingField = metaClass.Fields[name];
            if (existingField == null)
            {
                var attributes = new Mediachase.BusinessFoundation.Data.Meta.Management.AttributeCollection
                {
                    { McDataTypeAttribute.BooleanLabel, friendlyName },
                    { McDataTypeAttribute.EnumEditable, true }
                };

                metaClass.CreateMetaField(name, friendlyName, typeName, attributes); // DeleteMetaField
                using (var myEditScope = DataContext.Current.MetaModel.BeginEdit())
                {
                    metaClass.Fields[name].AccessLevel = AccessLevel.Development;
                    metaClass.Fields[name].Owner = "Development";
                    myEditScope.SaveChanges();
                }

                return String.Format("Meta field {0} is added to meta class {1}", name, OrganizationEntity.ClassName);
            }
            else
            {
                return String.Format("Meta field {0} is already exist in meta class {1}", name, OrganizationEntity.ClassName);
            }
        }

        string AddMetaFieldCheckboxBooleanContact()
        {
            string name = "Test";
            string friendlyName = name;
            var typeName = MetaFieldType.CheckboxBoolean;

            var metaClassName = ContactEntity.ClassName;
            var metaClass = DataContext.Current.MetaModel.MetaClasses[metaClassName];

            var existingField = metaClass.Fields[name];
            if (existingField == null)
            {
                var attributes = new Mediachase.BusinessFoundation.Data.Meta.Management.AttributeCollection
                {
                    { McDataTypeAttribute.BooleanLabel, friendlyName },
                    { McDataTypeAttribute.EnumEditable, true }
                };

                metaClass.CreateMetaField(name, friendlyName, typeName, attributes); // DeleteMetaField
                using (var myEditScope = DataContext.Current.MetaModel.BeginEdit())
                {
                    metaClass.Fields[name].AccessLevel = AccessLevel.Development;
                    metaClass.Fields[name].Owner = "Development";
                    myEditScope.SaveChanges();
                }

                return String.Format("Meta field {0} is added to meta class {1}", name, metaClassName);
            }
            else
            {
                return String.Format("Meta field {0} is already exist in meta class {1}", name, metaClassName);
            }
        }
    }
}

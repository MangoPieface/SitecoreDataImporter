using System;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.SharedSource.DataImporter.Providers;

namespace Sitecore.SharedSource.DataImporter.Mappings.Fields 
{	
    /// <summary>
    /// this field uses the url stored in the field and converts it to a LinkField value
    /// </summary>

    public class UrlToLink : ToText 
    {
		#region Properties 

		#endregion Properties
		
		#region Constructor

		//constructor
		public UrlToLink(Item i)
			: base(i) {
			
		}

		#endregion Constructor
		
		#region Methods

        public override void FillField(BaseDataMap map, ref Item newItem, string importValue)
        {
            //get the field as a link field and store the url
            LinkField lf = newItem.Fields[NewItemField];
            if (lf != null)
            {
                Guid importedGuidLink;
                if (IsSitecoreInternalLink(importValue, out importedGuidLink))
                {
                    ID importedSitecoreId = new ID(importedGuidLink);
                    lf.TargetID = importedSitecoreId;
                    lf.LinkType = "internal";
                }
                else
                {
                    lf.Url = importValue;
                }
            }
        }

        /// <summary>
        /// if we can parse this as a guid then assume we're importing a Sitecore item; this is useful if you are importing a list
        /// of 301 redirects where the Sitecore content already exists but the 301 redirect doesn't
        /// </summary>
        /// <param name="importValue"></param>
        /// <param name="importedGuidLink"></param>
        /// <returns></returns>
        private bool IsSitecoreInternalLink(string importValue, out Guid importedGuidLink)
        {
            return Guid.TryParse(importValue, out importedGuidLink);
        }

        #endregion Methods
	}
}
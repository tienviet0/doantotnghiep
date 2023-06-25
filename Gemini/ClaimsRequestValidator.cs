using System;
using System.Web;
using System.Web.Util;
using Microsoft.IdentityModel.Protocols.WSFederation;

namespace Gemini.IdentityModel
{
    public class ClaimsRequestValidator : RequestValidator
    {
        protected override bool IsValidRequestString(HttpContext context, string value,
                                                     RequestValidationSource requestValidationSource,
                                                     string collectionKey, out int validationFailureIndex)
        {
            validationFailureIndex = 0;

            if (requestValidationSource == RequestValidationSource.Form && collectionKey != null &&
                collectionKey.Equals(WSFederationConstants.Parameters.Result, StringComparison.Ordinal))
            {
                SignInResponseMessage message =
                    WSFederationMessage.CreateFromFormPost(context.Request) as SignInResponseMessage;

                if (message != null)
                {
                    return true;
                }
            }
            return base.IsValidRequestString(context, value, requestValidationSource, collectionKey,
                                             out validationFailureIndex);
        }
    }
}
#pragma checksum "C:\Users\Nensi Dedakia\source\repos\Helperland\Helperland\Views\Customer\CustomerServiceHistory.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "55ff0fffec13b043d427374dd6ebf6f51afbb524"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Customer_CustomerServiceHistory), @"mvc.1.0.view", @"/Views/Customer/CustomerServiceHistory.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\Nensi Dedakia\source\repos\Helperland\Helperland\Views\_ViewImports.cshtml"
using Helperland;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Nensi Dedakia\source\repos\Helperland\Helperland\Views\_ViewImports.cshtml"
using Helperland.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"55ff0fffec13b043d427374dd6ebf6f51afbb524", @"/Views/Customer/CustomerServiceHistory.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b5f94cf04a7ec23f27ac33992ef127038e0b3154", @"/Views/_ViewImports.cshtml")]
    public class Views_Customer_CustomerServiceHistory : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<Helperland.Models.Data.ServiceRequest>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/images/calendar.png"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/images/cap.png"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("cap"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/images/star1.png"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/images/star2.png"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"
<div>
    <p class=""Service-history-heading "">
        Service History
        <button type=""button"" class=""export-button"">Export</button>
    </p>
</div>
<table class=""example table upcoming-table"" style=""width: 100%;"">
    <thead>
        <tr>
            <th>Service Details</th>
            <th>Service Provider</th>
            <th>Payment</th>
            <th>Status</th>
            <th>Rate SP</th>

        </tr>
    </thead>
    <tbody>

");
#nullable restore
#line 22 "C:\Users\Nensi Dedakia\source\repos\Helperland\Helperland\Views\Customer\CustomerServiceHistory.cshtml"
         foreach (var req in Model)
        {
            if (req.Status == 1 || req.Status == 3)
            {
                DateTime dt = req.ServiceStartDate;               //Gets the current date
                string datetime = dt.ToString();          //converts the datetime value to string
                string[] DateTime = datetime.Split(' ');  //splitting the date from time with the help of space delimeter
                string Date = DateTime[0];                //saving the date value from the string array
                string Time = DateTime[1];
                string[] time = Time.Split(':');
                string clocktime = time[0] + ":" + time[1];
                var Extratime = req.ServiceHours + req.ExtraHours;
                string Endtime = Extratime.ToString();
                string totaltime = clocktime + Endtime;

#line default
#line hidden
#nullable disable
            WriteLiteral("                <tr class=\"customer-table-data\">\r\n                    <td class=\"service-detail\"");
            BeginWriteAttribute("onclick", " onclick=\"", 1494, "\"", 1541, 3);
            WriteAttributeValue("", 1504, "ServiceDetails(", 1504, 15, true);
#nullable restore
#line 37 "C:\Users\Nensi Dedakia\source\repos\Helperland\Helperland\Views\Customer\CustomerServiceHistory.cshtml"
WriteAttributeValue("", 1519, req.ServiceRequestId, 1519, 21, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 1540, ")", 1540, 1, true);
            EndWriteAttribute();
            WriteLiteral(" data-toggle=\"modal\" data-target=\".ServiceHistory-Servicedetails-modal\">\r\n                        <div>\r\n                            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "55ff0fffec13b043d427374dd6ebf6f51afbb5247363", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                            <b>");
#nullable restore
#line 40 "C:\Users\Nensi Dedakia\source\repos\Helperland\Helperland\Views\Customer\CustomerServiceHistory.cshtml"
                          Write(Html.DisplayFor(modelitem => Date));

#line default
#line hidden
#nullable disable
            WriteLiteral("</b>\r\n                        </div>\r\n                        <div>\r\n                            <b> ");
#nullable restore
#line 43 "C:\Users\Nensi Dedakia\source\repos\Helperland\Helperland\Views\Customer\CustomerServiceHistory.cshtml"
                           Write(Html.DisplayFor(modelitem => clocktime));

#line default
#line hidden
#nullable disable
            WriteLiteral("- ");
#nullable restore
#line 43 "C:\Users\Nensi Dedakia\source\repos\Helperland\Helperland\Views\Customer\CustomerServiceHistory.cshtml"
                                                                     Write(Html.DisplayFor(modelitem => totaltime));

#line default
#line hidden
#nullable disable
            WriteLiteral("</b>\r\n                        </div>\r\n                    </td>\r\n                    <td>\r\n                        <div>\r\n                            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "55ff0fffec13b043d427374dd6ebf6f51afbb5249603", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                            Lyum Watson\r\n                        </div>\r\n                        <div class=\"rating-star\">\r\n                            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "55ff0fffec13b043d427374dd6ebf6f51afbb52410877", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "55ff0fffec13b043d427374dd6ebf6f51afbb52411937", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "55ff0fffec13b043d427374dd6ebf6f51afbb52412997", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "55ff0fffec13b043d427374dd6ebf6f51afbb52414057", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "55ff0fffec13b043d427374dd6ebf6f51afbb52415117", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(" 4\r\n\r\n                        </div>\r\n\r\n                    </td>\r\n\r\n                    <td>\r\n                        <p class=\"euro-currency\">€<b class=\"rupee\">");
#nullable restore
#line 63 "C:\Users\Nensi Dedakia\source\repos\Helperland\Helperland\Views\Customer\CustomerServiceHistory.cshtml"
                                                              Write(Html.DisplayFor(modelitem => req.TotalCost));

#line default
#line hidden
#nullable disable
            WriteLiteral("</b></p>\r\n                    </td>\r\n                    <td>\r\n");
#nullable restore
#line 66 "C:\Users\Nensi Dedakia\source\repos\Helperland\Helperland\Views\Customer\CustomerServiceHistory.cshtml"
                         if (req.Status == 1)
                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                            <button type=\"button\" class=\"completed-button\">Completed</button>\r\n");
#nullable restore
#line 69 "C:\Users\Nensi Dedakia\source\repos\Helperland\Helperland\Views\Customer\CustomerServiceHistory.cshtml"
                        }
                        else
                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                            <button type=\"button\" class=\"cancelled-button\">Cancelled</button>\r\n");
#nullable restore
#line 73 "C:\Users\Nensi Dedakia\source\repos\Helperland\Helperland\Views\Customer\CustomerServiceHistory.cshtml"
                        }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n");
#nullable restore
#line 77 "C:\Users\Nensi Dedakia\source\repos\Helperland\Helperland\Views\Customer\CustomerServiceHistory.cshtml"
                         if (req.Status == 1)
                        {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                            <button type=""button"" class=""Rate-sp-button"" style=""background-color: #1d7A8C;"">
                                <a href=""#"" data-toggle=""modal"" data-target=""#RateSp"" style=""color:white; "">Rate SP</a>
                            </button>
");
#nullable restore
#line 82 "C:\Users\Nensi Dedakia\source\repos\Helperland\Helperland\Views\Customer\CustomerServiceHistory.cshtml"
                        }
                        else
                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                            <button type=\"button\" class=\"Rate-sp-button\" style=\"background-color: #6da9b5;\" disabled>\r\n                                <a style=\"color:white;\">Rate SP</a>\r\n                            </button>\r\n");
#nullable restore
#line 88 "C:\Users\Nensi Dedakia\source\repos\Helperland\Helperland\Views\Customer\CustomerServiceHistory.cshtml"
                        }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </td>\r\n                </tr>\r\n");
#nullable restore
#line 92 "C:\Users\Nensi Dedakia\source\repos\Helperland\Helperland\Views\Customer\CustomerServiceHistory.cshtml"

            }
        }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"    </tbody>

</table>

<div class=""container remove-modal"">
    <div class=""modal"" id=""RateSp"">
        <div class=""modal-dialog modal-dialog-centered"">
            <div class=""modal-content"">

                <!-- Modal Header -->
                <div class=""modal-header"">

                    <button type=""button"" class=""close"" data-dismiss=""modal"">&times;</button>
                </div>

                <!-- Modal body -->
                <div class=""modal-body"">
                    <div>
                        <img class=""cap"" src=""../images/cap.png"" style=""float: left;"">
                    </div>
                    <p>
                        <strong>Sandip Patel</strong><br><img src=""../images/grey-small-star.png""><img src=""../images/grey-small-star.png"">
                        <img src=""../images/grey-small-star.png""><img src=""../images/grey-small-star.png""><img src=""../images/grey-small-star.png""><span>&nbsp3.67</span>
                    </p>
                    <p>Rate Y");
            WriteLiteral(@"our Service Provider</p>
                    <hr>

                    <p>
                        On Time Arrival<span>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <i class=""fa fa-star"" aria-hidden=""true"" id=""st1""></i>
                            <i class=""fa fa-star"" aria-hidden=""true"" id=""st2""></i>
                            <i class=""fa fa-star"" aria-hidden=""true"" id=""st3""></i>
                            <i class=""fa fa-star"" aria-hidden=""true"" id=""st4""></i>
                            <i class=""fa fa-star"" aria-hidden=""true"" id=""st5""></i>
");
            WriteLiteral(@"                        </span>
                    </p>

                    <p>
                        Friendly<span>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img src=""../images/grey-small-star.png""><img src=""../images/grey-small-star.png"">
                            <img src=""../images/grey-small-star.png""><img src=""../images/grey-small-star.png""><img src=""../images/grey-small-star.png"">
                        </span>
                    </p>

                    <p>
                        Quality Of Service<span>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img src=""../images/grey-small-star.png""><img src=""../images/grey-small-star.png"">
                            <img src=""../images/grey-small-star.png""><img src=""../images/grey-small-star.png""><img src=""../images/grey-small-star.png"">
                        </span>
                    </p>

                    <p>Feedback on Service Provider</p>
                    <textarea rows=""2"" cols=""45""></tex");
            WriteLiteral(@"tarea>
                </div>
                <button type=""button"" class=""rounded-pill border border-1 text-center reschedulebtn"">Submit</button>
            </div>
        </div>
    </div>
</div>
<!--service details modal-->
<div class=""container Servicedetails-modal "">
    <div class=""modal ServiceHistory-Servicedetails-modal"">
        <div class=""modal-dialog modal-dialog-centered"">
            <div class=""modal-content"">
                <!-- Modal Header -->
                <div class=""modal-header"">
                    <h4 class=""modal-title text-center"">Service Details</h4>
                    <button type=""button"" class=""close"" data-dismiss=""modal"">&times;</button>
                </div>
                <!-- Modal body -->
                <div class=""modal-body"">
                    <div>
                        <span id=""dates""></span>
                        <span id=""times""></span>
                        <p><b>Duration:</b><span id=""durations"">&nbsp;</span></p>
            ");
            WriteLiteral(@"        </div>
                    <hr>
                    <div>
                        <p><b>ServiceId: </b> <span id=""IDs""></span></p>
                        <p><b>Extras: </b> <span id=""Extras""></span></p>
                        <p><b>Net Amount:</b> <span class=""amt"" id=""NetAmounts"">&nbsp;</span></p>
                    </div>
                    <hr>
                    <div>
                        <p><b>Service Address:</b> <span id=""ServiceAddresss"">&nbsp;</span></p>
                        <p><b>Billing Address:</b> <span>&nbsp;Same as Service Address</span></p>
                        <p><b>Phone: </b> <span id=""Phones"">&nbsp;</span></p>
                        <p><b>Email: </b> <span id=""Emails"">&nbsp;</span></p>
                    </div>
                    <hr>
                    <div>
                        <p><b>Comments</b></p>
                        <span id=""Comments"">I don't have Pets at home</span>
                    </div>
                    <hr>

          ");
            WriteLiteral(@"      </div>
            </div>
        </div>
    </div>
</div>

<script type=""text/javascript"">
    function ServiceDetails(Id) {

        $.post(""/Customer/CustomerServiceDetail"", { Id: parseInt(Id) }, function (data) {

            $(""#IDs"").html(data.id);

            $(""#Extras"").html(data.extra);

            $(""#NetAmounts"").html(data.netamount);
            $(""#ServiceAddresss"").html(data.serviceaddress);
            $(""#Phones"").html(data.phone);
            $(""#Emails"").html(data.email);
            $(""#Comments"").html(data.comments);
            $(""#dates"").html(data.servicedate);
            $(""#times"").html(data.servicetime);
            $(""#durations"").html(data.duration);
            //$(""#Reschedule_Btn"").prop(""value"", data.cid);
            //$(""#Cancel_Btn"").prop(""value"", data.cid);

        });


    }
</script>
");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<Helperland.Models.Data.ServiceRequest>> Html { get; private set; }
    }
}
#pragma warning restore 1591

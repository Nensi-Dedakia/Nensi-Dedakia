#pragma checksum "C:\Users\Nensi Dedakia\source\repos\Helperland\Helperland\Views\ServiceProvider\BlockCustomer.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "b7a8475be7f24d3b0acbe217110a2d38eac8e4de"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_ServiceProvider_BlockCustomer), @"mvc.1.0.view", @"/Views/ServiceProvider/BlockCustomer.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b7a8475be7f24d3b0acbe217110a2d38eac8e4de", @"/Views/ServiceProvider/BlockCustomer.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b5f94cf04a7ec23f27ac33992ef127038e0b3154", @"/Views/_ViewImports.cshtml")]
    public class Views_ServiceProvider_BlockCustomer : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<Helperland.ViewModels.UpcomingViewModel>>
    {
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n\r\n\r\n");
#nullable restore
#line 5 "C:\Users\Nensi Dedakia\source\repos\Helperland\Helperland\Views\ServiceProvider\BlockCustomer.cshtml"
  
    Layout = null;

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<style type=""text/css"">
    .block-button {
        background-color: #ff6b6b;
        border: 1px solid #ff6b6b;
        color: white;
        border-radius: 27px;
        padding-top: 5px;
        padding-bottom: 5px;
        padding-left: 10px;
        padding-right: 10px;
    }

        .block-button:hover {
            text-decoration: none !important;
            color: white !important;
            background-color: #fa8072;
            border: 1px solid #fa8072;
        }



    .Avtar {
        height: 85px;
        width: 85px;
        margin-top: 30px;
        border-radius: 50%;
        border: 1px solid #C0C0C0;
        /* justify-content: center!important; */
        /*margin-left: 140px;
     margin-right: 40px; */
    }

        .Avtar img {
            height: 86px;
            width: 86px;
        }
</style>
");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "b7a8475be7f24d3b0acbe217110a2d38eac8e4de4345", async() => {
                WriteLiteral("\r\n");
                WriteLiteral("    <table class=\"table upcoming-table exams\" style=\"width: 100%;\">\r\n        <div class=\"row mb-4\">\r\n");
#nullable restore
#line 50 "C:\Users\Nensi Dedakia\source\repos\Helperland\Helperland\Views\ServiceProvider\BlockCustomer.cshtml"
             foreach (var block in Model)
            {


#line default
#line hidden
#nullable disable
                WriteLiteral("                <div class=\"col-sm-4\" id=\"BlockCustomer\">\r\n                    <div class=\"card  align-items-center\">\r\n                        <div class=\"Avtar \">\r\n                            <img src=\"../images/default-sp-xlarge.png\"");
                BeginWriteAttribute("class", " class=\"", 1392, "\"", 1400, 0);
                EndWriteAttribute();
                WriteLiteral(" alt=\"Avtar\">\r\n                        </div>\r\n\r\n                        <div class=\"card-body text-center\">\r\n                            <h5 class=\"card-title mb-3\"><b style=\"font-size: 16px; color: #646464; \">");
#nullable restore
#line 60 "C:\Users\Nensi Dedakia\source\repos\Helperland\Helperland\Views\ServiceProvider\BlockCustomer.cshtml"
                                                                                                Write(Html.DisplayFor(modelitem => block.user.FirstName));

#line default
#line hidden
#nullable disable
                WriteLiteral("  ");
#nullable restore
#line 60 "C:\Users\Nensi Dedakia\source\repos\Helperland\Helperland\Views\ServiceProvider\BlockCustomer.cshtml"
                                                                                                                                                     Write(Html.DisplayFor(modelitem => block.user.LastName));

#line default
#line hidden
#nullable disable
                WriteLiteral(" </b></h5>\r\n                            <div id=\"blockbtn\">\r\n\r\n                            </div>\r\n\r\n\r\n                            <a href=\"#\" class=\"block-button rounded-pill\"");
                BeginWriteAttribute("id", " id=\"", 1891, "\"", 1914, 1);
#nullable restore
#line 66 "C:\Users\Nensi Dedakia\source\repos\Helperland\Helperland\Views\ServiceProvider\BlockCustomer.cshtml"
WriteAttributeValue("", 1896, block.user.UserId, 1896, 18, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                BeginWriteAttribute("onclick", " onclick=\"", 1915, "\"", 1952, 3);
                WriteAttributeValue("", 1925, "spBlock(", 1925, 8, true);
#nullable restore
#line 66 "C:\Users\Nensi Dedakia\source\repos\Helperland\Helperland\Views\ServiceProvider\BlockCustomer.cshtml"
WriteAttributeValue("", 1933, block.user.UserId, 1933, 18, false);

#line default
#line hidden
#nullable disable
                WriteAttributeValue("", 1951, ")", 1951, 1, true);
                EndWriteAttribute();
                WriteLiteral(">Block</a>\r\n\r\n                            <script>\r\n                                $.post(\"/ServiceProvider/CheckBlock\", { Id: parseInt(");
#nullable restore
#line 69 "C:\Users\Nensi Dedakia\source\repos\Helperland\Helperland\Views\ServiceProvider\BlockCustomer.cshtml"
                                                                                Write(block.user.UserId);

#line default
#line hidden
#nullable disable
                WriteLiteral(") }, function (data) {\r\n                                    if (data == true) {\r\n                                        var x = \"#\" + ");
#nullable restore
#line 71 "C:\Users\Nensi Dedakia\source\repos\Helperland\Helperland\Views\ServiceProvider\BlockCustomer.cshtml"
                                                 Write(block.user.UserId);

#line default
#line hidden
#nullable disable
                WriteLiteral(" ;\r\n                                        $(x).html(\"UnBlock\");\r\n                                    }\r\n                                    else {\r\n                                        var x = \"#\" + ");
#nullable restore
#line 75 "C:\Users\Nensi Dedakia\source\repos\Helperland\Helperland\Views\ServiceProvider\BlockCustomer.cshtml"
                                                 Write(block.user.UserId);

#line default
#line hidden
#nullable disable
                WriteLiteral(@" ;
                                        $(x).html(""Block"");
                                    }
                                });
                            </script>

                        </div>
                    </div>

                </div>
");
#nullable restore
#line 85 "C:\Users\Nensi Dedakia\source\repos\Helperland\Helperland\Views\ServiceProvider\BlockCustomer.cshtml"

            }

#line default
#line hidden
#nullable disable
                WriteLiteral("        </div>\r\n    </table>\r\n");
                WriteLiteral("\r\n");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"

<script type=""text/javascript"">


    function spBlock(blockid) {
        $.post(""/ServiceProvider/BlockUser"", { Id: parseInt(blockid) }, function (data) {


            if (data) {
                alert(""You have Blocked Customer!!"");
                spBlockCustomer();
            }



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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<Helperland.ViewModels.UpcomingViewModel>> Html { get; private set; }
    }
}
#pragma warning restore 1591

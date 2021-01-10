<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SimplifiedSearch.aspx.cs" Inherits="MarketingAutomationTool.Leads.SimplifiedSearch" MasterPageFile="~/MATSite.Master" %>
<%@ Register Src="~/Leads/UserControls/Lead.ascx" TagName="LeadTopMenu" TagPrefix="uc" %>
<%@ Register Src="~/Leads/UserControls/LeftSearch.ascx" TagName="LeftSearch" TagPrefix="uc" %>
<%@ Register Src="~/UserControls/Pager.ascx" TagName="Pager" TagPrefix="uc" %>
<%@ Register Src="~/Leads/UserControls/ViewRecentSearches.ascx" TagName="ViewRecentSearches" TagPrefix="uc" %>
<%@ Register Src="~/UserControls/NewEmailCampaign.ascx" TagName="NewEmailCampaign" TagPrefix="uc" %>
<%@ Register Src="~/Leads/UserControls/Shortcuts.ascx" TagName="Shortcuts" TagPrefix="uc" %>
<%@ Register Src="~/Leads/UserControls/LeadLists.ascx" TagName="LeadLists" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cpHead" runat="server"> 
    <link rel="stylesheet" href="../dcjqaccordion-master/css/dcaccordion.css" />
    <link rel="stylesheet" href="../Styles/loader.css" />
     
    <script src="../jQuery/jquery-3.3.1.js" type="text/javascript"></script>
    <script src="../jQuery/jquery-3.3.1.min.js" type="text/javascript"></script>
	<script src="../dcjqaccordion-master/js/jquery.cookie.js" type="text/javascript"></script>
	<script src="../dcjqaccordion-master/js/jquery.hoverIntent.minified.js" type="text/javascript"></script>
	<script src="../dcjqaccordion-master/js/jquery.dcjqaccordion.2.7.min.js" type="text/javascript"></script>
	<script src="../dcjqaccordion-master/accordionmenu.js" type="text/javascript"></script>
	
	<script type="text/javascript">

        $(document).ready(function () {

            $.fn.SetOverlayHeightWidth = function () {
                $(this).height($(document).height());
                $(this).width($(document).width());
            };

            $("#lnkFilter").click(function () {
                $("#collapsePanel").toggleClass("show");
             }); 
			 
			if ($('.sidescroller').length) {
				$('.sidescroller').css('min-height', '360px');
				$('.sidescroller').css('width', 'auto');
			}
			
			$(".contacts-list li").hover(
				function () { $(this).children('.contactdetail-icons').show(); },
				function () 
				{ 
					$(this).children('.contactdetail-icons').hide(); 
					$('.sideMenu').tooltip('hide');
				}
			);
			
			if ($('.sideMenu').length) {
				try {
                    $('.sideMenu').tooltip();
                }
                catch (err) {

                };
            };

            if ($('.iconlink').length) {
                try {
                    $('.iconlink').tooltip();
                }
                catch (err) {

                };
            };

            $("#lnkSearch").click(function(){
                ShowListInformation();
                $("#<%=hdnActiveTab.ClientID%>").val("");
			});
			
			$("#lnkResults").click(function(){
                ShowSearchResults();
            });

            if ($("#<%=hdnActiveTab.ClientID%>").val() == "RESULTS") {
                 ShowSearchResults();
            }

            $("#dvLoading").hide();
            $("#<%=btnExport.ClientID%>").click(function () {
                $("#dvLoading").show();
                $(".overlayforaddNote").show().SetOverlayHeightWidth();
			});

            PopulateCountryDropDown();
            PopulateSelectListDropDown();
            PopulateSearchFields();

        });

        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;
            if ((charCode >= 48 && charCode <= 57)||charCode==45)
                return true;

            return false;
        }

        function PopulateSearchFields() {
            var searchfields = document.getElementById("<%= hdnSearchFields.ClientID%>").value;
            //alert(searchfields);
            if (searchfields !== '') {
                //alert(searchfields);
                var searchfieldsobject = JSON.parse(searchfields);
                var searchfieldsitems = searchfieldsobject.searchfieldsitem;
                var i = 0;
                var field = '';
                var operator = '';
                var searchvalue = '';
                var logicaloperator = '';

                //alert(searchfieldsitems.length);
                var prevfield = '';
                var curfield = '';
                var usestaticfield = 0;

                if (searchfieldsitems.length == 0) {
                     var selectinput = document.getElementById("ddlisdeleted");
                     selectinput.options[0].defaultSelected = true;
                }

                for (i = 0; i < searchfieldsitems.length; i++) {
                    field = searchfieldsitems[i].field;
                    operator = searchfieldsitems[i].operator;
                    searchvalue = searchfieldsitems[i].searchvalue;
                    logicaloperator = searchfieldsitems[i].logicaloperator;

                    if (i == 0) {
                        prevfield = field;
                        curfield = field;
                        usestaticfield = 1;
                    }
                    else {
                        curfield = field;
                        if (prevfield !== curfield) {
                            usestaticfield = 1;
                            prevfield = field;
                        }
                    } 
                    
                    if (usestaticfield == 1) {
                        AssignField(field, operator, searchvalue, logicaloperator);
                        usestaticfield = 0;
                    }
                    else {
                        AddField(field,operator,searchvalue,logicaloperator);
                    }
                }
            }
        }

        function ShowListInformation() {
               $("#lnkSearch").addClass("active");
				$("#tab_Search").addClass("active");
				$("#lnkResults").removeClass("active");
				$("#tab_Results").removeClass("active");
        }

        function ShowSearchResults() {
                $("#lnkSearch").removeClass("active");
				$("#tab_Search").removeClass("active");
				$("#lnkResults").addClass("active");
				$("#tab_Results").addClass("active");
        }

        function PopulateCountryDropDown() {
            var i = 0;
            var selectinputitems = [];
            selectinputitems = GetCountryList();

            var selectinput = document.getElementById("ddlcountry");
            for (i = 0; i < selectinputitems.length; i++) {
                    var selectinputOption = document.createElement("option");  
                    selectinputOption.text = selectinputitems[i].text;
                    selectinputOption.value = selectinputitems[i].value;

                    selectinput.add(selectinputOption, selectinput[i]);
                }
        }

        function PopulateSelectListDropDown() {
            var i = 0;
            var selectinputitems = [];
            selectinputitems = GetContactListList();

            var selectinput = document.getElementById("ddlcontactlist");
            for (i = 0; i < selectinputitems.length; i++) {
                    var selectinputOption = document.createElement("option");  
                    selectinputOption.text = selectinputitems[i].text;
                    selectinputOption.value = selectinputitems[i].value;

                    selectinput.add(selectinputOption, selectinput[i]);
                }
        }

        function GetCountryList() {
            var selectinputitems = [];
            var countrylistobj = JSON.parse(document.getElementById('<%= hdnCountryList.ClientID%>').value);
            var countrylistarray = countrylistobj.countrylist;

            selectinputitems.push({ "text": "", "value": "" });
            for (i = 0; i < countrylistarray.length; i++) {
	            var countryitem = countrylistarray[i];
                    selectinputitems.push({ "text": countryitem.text, "value": countryitem.value });
            }

            return selectinputitems;
        }

        function GetContactListList() {
            var selectinputitems = [];
            var contactlistobj = JSON.parse(document.getElementById('<%= hdnContactListList.ClientID%>').value);
            var contactlistarray = contactlistobj.contactlistlist;

            selectinputitems.push({ "text": "", "value": "" });
            for (i = 0; i < contactlistarray.length; i++) {
	            var contactlistitem = contactlistarray[i];
                    selectinputitems.push({ "text": contactlistitem.text, "value": contactlistitem.value });
            }

            return selectinputitems;
        }
	
        function ConfirmDelete() {
            return window.confirm("Are you sure you want to delete this lead?");
        }

        function BuiltInFieldsProperties() {
            var builtinfields = [
                { "field": "firstname", "properties": '{"indexfield":"hdnFirstNameIndex","type":"text", "label":"First Name","maxlength":"100", "allowmultiple":"yes", "optionsfunction":"none"}', "grouping": "builtinfields" },
                { "field": "middlename", "properties": '{"indexfield":"hdnMiddleNameIndex","type":"text", "label":"Middle Name","maxlength":"100", "allowmultiple":"yes", "optionsfunction":"none"}', "grouping": "builtinfields" },
                { "field": "lastname", "properties": '{"indexfield":"hdnLastNameIndex","type":"text", "label":"Last Name","maxlength":"100", "allowmultiple":"yes", "optionsfunction":"none"}', "grouping": "builtinfields" },
                { "field": "email", "properties": '{"indexfield":"hdnEmailIndex","type":"text", "label":"Email","maxlength":"250", "allowmultiple":"yes", "optionsfunction":"none"}', "grouping": "builtinfields" },
                { "field": "companyname", "properties": '{"indexfield":"hdnCompanyNameIndex","type":"text","label":"Company Name", "maxlength":"100", "allowmultiple":"yes", "optionsfunction":"none"}', "grouping": "builtinfields" },
                { "field": "position", "properties": '{"indexfield":"hdnPositionIndex","type":"text","label":"Position", "maxlength":"50", "allowmultiple":"yes", "optionsfunction":"none"}', "grouping": "builtinfields" },
                { "field": "website", "properties": '{"indexfield":"hdnWebsiteIndex","type":"text", "label":"Website","maxlength":"250", "allowmultiple":"yes", "optionsfunction":"none"}', "grouping": "builtinfields" },
                { "field": "mobile", "properties": '{"indexfield":"hdnMobileIndex","type":"text", "label":"Mobile","maxlength":"100", "allowmultiple":"yes", "optionsfunction":"none"}', "grouping": "builtinfields" },
                { "field": "phoneno", "properties": '{"indexfield":"hdnPhoneNoIndex","type":"text", "label":"Phone No","maxlength":"100", "allowmultiple":"yes", "optionsfunction":"none"}', "grouping": "builtinfields" },
                { "field": "address", "properties": '{"indexfield":"hdnAddressIndex","type":"text", "label":"Address","maxlength":"501", "allowmultiple":"yes", "optionsfunction":"none"}', "grouping": "builtinfields" },
                { "field": "city", "properties": '{"indexfield":"hdnCityIndex","type":"text", "label":"City","maxlength":"50", "allowmultiple":"yes", "optionsfunction":"none"}', "grouping": "builtinfields" },
                { "field": "state", "properties": '{"indexfield":"hdnStateIndex","type":"text", "label":"State","maxlength":"50", "allowmultiple":"yes", "optionsfunction":"none"}', "grouping": "builtinfields" },
                { "field": "country", "properties": '{"indexfield":"hdnCountryIndex","type":"select", "label":"Country","maxlength":"0", "allowmultiple":"yes", "optionsfunction":"GetCountries()"}', "grouping": "builtinfields" },
                { "field": "contacttype", "properties": '{"indexfield":"none","type":"select","label":"none", "maxlength":"0", "allowmultiple":"no", "optionsfunction":"none"}', "grouping": "builtinfields" },
                { "field": "contactstatus", "properties": '{"indexfield":"hdnStatusIndex","type":"select", "label":"Status","maxlength":"0", "allowmultiple":"yes", "optionsfunction":"GetStatuses()"}', "grouping": "builtinfields" },
                { "field": "gender", "properties": '{"indexfield":"none","type":"select", "label":"none","maxlength":"0", "allowmultiple":"no", "optionsfunction":"none"}', "grouping": "builtinfields" },
                { "field": "subscribedtoemail", "properties": '{"indexfield":"none","type":"select", "label":"none","maxlength":"0", "allowmultiple":"no", "optionsfunction":"none"}', "grouping": "builtinfields" },
                { "field": "isdeleted", "properties": '{"indexfield":"none","type":"select", "label":"none","maxlength":"0", "allowmultiple":"no", "optionsfunction":"none"}', "grouping": "builtinfields" },
                { "field": "usefortesting", "properties": '{"indexfield":"none","type":"select", "label":"none","maxlength":"0", "allowmultiple":"no", "optionsfunction":"none"}', "grouping": "builtinfields" },
                { "field": "contactlist", "properties": '{"indexfield":"hdnContactListIndex","type":"select", "label":"Select a List","maxlength":"0", "allowmultiple":"yes", "optionsfunction":"GetContactListList()"}', "grouping": "lists" }
            ];

            return builtinfields;
        }

        function StyleProperties() {

            var styleproperties=[
                {"grouping":"builtinfields", "hasop":"yes", "classes":'{"dvlbl":"col-sm-3 text-right","cnlbl":"col-form-label","dvop":"col-sm-2","cnop":"form-control","dvin":"col-sm-4","cnin":"form-control","dvlogop":"col-sm-2","cnlogop":"form-control","dvrembtn":"col-sm-1 pull-left","cnrembtn":"btn btn-secondary"}'},
                {"grouping":"lists", "hasop":"no", "classes":'{"dvlbl":"col-sm-2 text-right","cnlbl":"col-form-label","dvop":"na","cnop":"na","dvin":"col-sm-7","cnin":"form-control","dvlogop":"col-sm-2","cnlogop":"form-control","dvrembtn":"col-sm-1 pull-left","cnrembtn":"btn btn-secondary"}'}
            ]

            return styleproperties;
        }

        function AssignField(name, operator = "", searchvalue = "", logicaloperator = "") {
            var builtinfields = BuiltInFieldsProperties();
            var fieldproperties_string;
            var fieldproperties_json;
            var i = 0;

            for (i = 0; i < builtinfields.length; i++) {
                if (builtinfields[i].field == name) {
                    fieldproperties_string = builtinfields[i].properties;
                    break;
                }
            }

            fieldproperties_json = JSON.parse(fieldproperties_string);

            var selectOpIndex = 0;
            if (document.getElementById("operator_" + name)) {
                var ddlOperator = document.getElementById("operator_" + name);
                for (var i = 0; i < ddlOperator.options.length; i++) {
                    ddlOperator.options[i].defaultSelected = operator == ddlOperator.options[i].text;

                    if (operator == ddlOperator.options[i].text) {
                        selectOpIndex = i;
                    }
                }
            }

            if (fieldproperties_json.type == "text") {
                if (document.getElementById("txt" + name)) {
                    var txtinput = document.getElementById("txt" + name);
                    txtinput.value = searchvalue;
                    if (document.getElementById("operator_" + name)) {
                        var ddlOperator = document.getElementById("operator_" + name);
                        if ((ddlOperator.options[selectOpIndex].text == 'IS NULL') || (ddlOperator.options[selectOpIndex].text == 'IS NOT NULL')) {
                            txtinput.disabled = true;
                        }
                        else {
                            txtinput.disabled = false;
                        }
                    }

                }
            }
            else if (fieldproperties_json.type == "select") {
                if (document.getElementById("ddl" + name)) {
                    var selectinput = document.getElementById("ddl" + name);
                    for (var i = 0; i < selectinput.options.length; i++) {
                        selectinput.options[i].defaultSelected = searchvalue == selectinput.options[i].value;
                    }

                    if (document.getElementById("operator_" + name)) {
                        var ddlOperator = document.getElementById("operator_" + name);
                        if ((ddlOperator.options[selectOpIndex].text == 'IS NULL') || (ddlOperator.options[selectOpIndex].text == 'IS NOT NULL')) {
                            selectinput.disabled = true;
                        }
                        else {
                            selectinput.disabled = false;
                        }
                    }
                }
            }

            if (document.getElementById("logicaloperator_" + name)) {
                var dvLogicalOperator = document.getElementById("logicaloperator_" + name);
                for (var i = 0; i < dvLogicalOperator.options.length; i++) {
                    dvLogicalOperator.options[i].defaultSelected = logicaloperator == dvLogicalOperator.options[i].text;
                }
            }

        }

        function AddField(name,operator="", searchvalue="",logicaloperator="") {
            var builtinfields = BuiltInFieldsProperties();
            var styleproperties = StyleProperties();
            var fieldproperties_string;
            var fieldproperties_json;
            var fieldproperties_grouping;
            var group_style;
            var group_hasoperator;
            var group_style_json;
            var i = 0;

            for (i = 0; i < builtinfields.length; i++) {
                if (builtinfields[i].field == name) {
                    fieldproperties_string = builtinfields[i].properties;
                    fieldproperties_grouping = builtinfields[i].grouping;
                    break;
                }
            }

            fieldproperties_json = JSON.parse(fieldproperties_string);

            for (i = 0; i < styleproperties.length; i++) {
                if (styleproperties[i].grouping == fieldproperties_grouping) {
                    group_style = styleproperties[i].classes;
                    group_hasoperator = styleproperties[i].hasop;
                    break;
                }
            }

             group_style_json = JSON.parse(group_style);

            if (fieldproperties_json.indexfield !== 'none') {
                var fieldindex=document.getElementById(fieldproperties_json.indexfield).value;
                if (fieldindex == "") fieldindex = "1";
                else fieldindex = (parseInt(fieldindex) + 1).toString();
                document.getElementById(fieldproperties_json.indexfield).value = fieldindex;  

                var dvContainer = document.getElementById("dv_addl_"+name);

                var dvNewRow = document.createElement("div");
                dvNewRow.id = "dv" + name + fieldindex;
                dvNewRow.className="row samefieldsearchpad";

                var dvLabel = document.createElement("div");
                dvLabel.className=group_style_json.dvlbl;

                var lblFieldName = document.createElement("label");
                lblFieldName.className=group_style_json.cnlbl;
                lblFieldName.innerHTML =fieldproperties_json.label;

                dvLabel.append(lblFieldName);

                var dvOperator = document.createElement("div");
                if (group_hasoperator == 'yes') {
                    dvOperator.className=group_style_json.dvop;

                    var ddlOperator = document.createElement("select");
                    ddlOperator.id = "operator_" + name + fieldindex;
                    ddlOperator.className = group_style_json.cnop;

                    var optionsOperators = [{ "text": "LIKE", "value": "LIKE" },
                        { "text": "NOT LIKE", "value": "NOT LIKE" },
                        { "text": "=", "value": "=" },
                        { "text": "<>", "value": "<>" },
                        { "text": "IS NULL", "value": "IS NULL" },
                        { "text": "IS NOT NULL", "value": "IS NOT NULL" },
                        { "text": "<", "value": "<" },
                        { "text": "<=", "value": "<=" },
                        { "text": ">", "value": ">" },
                        { "text": ">=", "value": ">=" }];

                    var selectOpIndex = 0;

                    for (i = 0; i < optionsOperators.length; i++) {
                        var ddlOperatorOption = document.createElement("option");

                        if (operator !== '') {
                            if (operator == optionsOperators[i].text) {
                                selectOpIndex = i;
                            }
                        }

                        ddlOperatorOption.text = optionsOperators[i].text;
                        ddlOperatorOption.value = optionsOperators[i].value;

                        ddlOperator.add(ddlOperatorOption, ddlOperator[i]);
                    }

                    if (selectOpIndex !== 0) {
                       ddlOperator.options[selectOpIndex].defaultSelected = true;
                    }

                    ddlOperator.onchange = function () {
                        operatoronchange(ddlOperator, fieldproperties_json.type);
                    }

                    dvOperator.append(ddlOperator);
                }

                var dvInput = document.createElement("div");
                dvInput.className = group_style_json.dvin;

                if (fieldproperties_json.type == "text") {
                    var txtinput = document.createElement("input");
                    txtinput.setAttribute("type", "text");
                    txtinput.setAttribute("maxlength", fieldproperties_json.maxlength);
                    txtinput.className =  group_style_json.cnin;
                    txtinput.id = "txt" + name + fieldindex;

                    if (searchvalue !== '') {
                        txtinput.value = searchvalue;
                    }

                    if ((operator == 'IS NULL') || (operator == 'IS NOT NULL')) {
                        txtinput.disabled = true;
                    }
                    else {
                        txtinput.disabled = false;
                    }


                    dvInput.append(txtinput);
                }
                else if(fieldproperties_json.type == "select") {

                    var selectinput = document.createElement("select");
                    selectinput.className = group_style_json.cnin;
                    selectinput.id = "ddl" + name + fieldindex;


                    var methodname = fieldproperties_json.optionsfunction;
                    var selectinputitems = [];

                    if (methodname == "GetCountries()") {

                        selectinputitems = GetCountryList();

                    }
                    else if (methodname == "GetStatuses()") {
                        selectinputitems.push({ "text": "", "value": "" });
                        selectinputitems.push({ "text": "Not Confirmed", "value": "1" });
                        selectinputitems.push({ "text": "Confirmed", "value": "2" });
                        selectinputitems.push({ "text": "Active", "value": "3" });
                        selectinputitems.push({ "text": "Inactive", "value": "4" });
                    }
                    else if (methodname == "GetTypes()") {
                        selectinputitems.push({ "text": "", "value": "" });
                        selectinputitems.push({ "text": "Lead", "value": "1" });
                        selectinputitems.push({ "text": "Contact", "value": "2" });
                    }
                    else if (methodname == "GetGenders()") {
                        selectinputitems.push({ "text": "", "value": "" });
                        selectinputitems.push({ "text": "Male", "value": "M" });
                        selectinputitems.push({ "text": "Female", "value": "F" });
                    }
                    else if (methodname == "GetContactListList()") {
                         selectinputitems = GetContactListList();
                    }

                    var selectValueIndex = 0;
                    for (i = 0; i < selectinputitems.length; i++) {
                        var selectinputOption = document.createElement("option");  

                       if (searchvalue !== '') {
                            if (searchvalue == selectinputitems[i].value) {
                                selectValueIndex = i;
                            }
                        }

                        selectinputOption.text = selectinputitems[i].text;
                        selectinputOption.value = selectinputitems[i].value;

                        selectinput.add(selectinputOption, selectinput[i]);
                    }

                    if (selectValueIndex !== 0) {
                        selectinput.options[selectValueIndex].defaultSelected = true;
                    }

                    if ((operator == 'IS NULL') || (operator == 'IS NOT NULL')) {
                        selectinput.disabled = true;
                    }
                    else {
                        selectinput.disabled = false;
                    }

                    dvInput.append(selectinput);
                }

                var dvLogicalOperator = document.createElement("div");
                dvLogicalOperator.className = group_style_json.dvlogop;

                var ddlLogicalOperator = document.createElement("select");
                ddlLogicalOperator.id = "logicaloperator_" + name + fieldindex;
                ddlLogicalOperator.className = group_style_json.cnlogop;

                var optionsLogicalOperators = [{ "text": "AND", "value": "AND" },
                    { "text": "OR", "value": "OR" }];

                var selectLogOpIndex = 0;
                for (i = 0; i < optionsLogicalOperators.length; i++) {
                    var ddlLogicalOperatorOption = document.createElement("option");

                     if (logicaloperator !== '') {
                        if (logicaloperator == optionsLogicalOperators[i].text) {
                            selectLogOpIndex = i;
                        }
                     }

                    ddlLogicalOperatorOption.text = optionsLogicalOperators[i].text;
                    ddlLogicalOperatorOption.value = optionsLogicalOperators[i].value;

                    ddlLogicalOperator.add(ddlLogicalOperatorOption, ddlLogicalOperator[i]);
                }

                if (selectLogOpIndex !== 0) {
                        ddlLogicalOperator.options[selectLogOpIndex].defaultSelected = true;
                }

                dvLogicalOperator.append(ddlLogicalOperator);

                var dvRemove = document.createElement("div");
                dvRemove.className = group_style_json.dvrembtn;

                var aRemove = document.createElement("a");
                aRemove.className = group_style_json.cnrembtn;
                aRemove.setAttribute("href","#dvgroup"+name);
                aRemove.setAttribute("onclick","RemoveField('dv" + name + fieldindex + "')");
                aRemove.innerHTML = "<i class='fas fa-trash-alt'></i>";

                dvRemove.append(aRemove);

                dvNewRow.append(dvLabel);
                if (group_hasoperator == 'yes') {
                   dvNewRow.append(dvOperator);
                }
                dvNewRow.append(dvInput);
                dvNewRow.append(dvLogicalOperator);
                dvNewRow.append(dvRemove);
                dvContainer.append(dvNewRow);
            } 
        }

        function RemoveField(name) {
            var dvToRemove = document.getElementById(name);
            dvToRemove.remove();
        }

        function GetSearchValues() {
            var builtinfields = BuiltInFieldsProperties();
            iterateSearchFields(builtinfields);
        }


        function iterateSearchFields(builtinfields) {

            var builtinfields_index = 0;
            var searchfields = {
                searchfieldsitem : []
            };

            for (builtinfields_index = 0; builtinfields_index < builtinfields.length; builtinfields_index++) {

                var item = builtinfields[builtinfields_index];
                var fieldname = item.field;
                var properties_string = item.properties;
                var properties = JSON.parse(properties_string);

                var controlname = '';
                var opname = '';
                var logopname = '';
                var operatorvalue = '';
                var logopname_value = '';

                if (document.getElementById(properties.indexfield)) {
                    var maxcount_string = document.getElementById(properties.indexfield).value;
                    var maxcount = 0;
                    if (maxcount_string !== '') {
                        maxcount = parseInt(maxcount_string);
                    };

                    var i = 0;

                    for (i = 0; i <= maxcount; i++) {
                        if (properties.type == "text") {
                            controlname = "txt" + fieldname;
                        }
                        else if (properties.type == "select") {
                            controlname = "ddl" + fieldname;
                        }

                        opname = "operator_" + fieldname;
                        logopname = "logicaloperator_" + fieldname;

                        if (i > 0) {
                            controlname += i.toString();
                            opname += i.toString();
                            logopname += i.toString();
                        }

                        if (document.getElementById(logopname)) {
                            logopname_value = document.getElementById(logopname).value;
                        }
                        else {
                            logopname_value = "AND";
                        }

                        

                        if (document.getElementById(controlname)) {
                            if (document.getElementById(opname)) {
                                if ((document.getElementById(opname).value !== 'IS NULL') && (document.getElementById(opname).value !== 'IS NOT NULL')) {
                                    if (document.getElementById(controlname).value !== '') {
                                        searchfields.searchfieldsitem.push({
                                            "field": fieldname,
                                            "operator": document.getElementById(opname).value,
                                            "searchvalue": document.getElementById(controlname).value,
                                            "logicaloperator": logopname_value
                                        });
                                    }
                                }
                                else {
                                    searchfields.searchfieldsitem.push({
                                        "field": fieldname,
                                        "operator": document.getElementById(opname).value,
                                        "searchvalue": '',
                                        "logicaloperator": logopname_value
                                    });
                                }
                            }
                            else {//lists - no operator
                                if (fieldname == 'contactlist') {
                                    if (document.getElementById(controlname).value != '') {
                                        if (document.getElementById(controlname).value == '-1') {
                                            searchfields.searchfieldsitem.push({
                                                "field": fieldname,
                                                "operator": 'IS NULL',
                                                "searchvalue": document.getElementById(controlname).value,
                                                "logicaloperator": logopname_value
                                            });
                                        }
                                        else {
                                            searchfields.searchfieldsitem.push({
                                                "field": fieldname,
                                                "operator": '=',
                                                "searchvalue": document.getElementById(controlname).value,
                                                "logicaloperator": logopname_value
                                            });

                                        }
                                    }
                                }
                            }
                        }
                        
                    }
                }
                else {

                        if (properties.type == "text") {
                            controlname = "txt" + fieldname;
                        }
                        else if (properties.type == "select") {
                            controlname = "ddl" + fieldname;
                        }

                        opname = "operator_" + fieldname;
                        logopname = "logicaloperator_" + fieldname;

                        if (document.getElementById(opname)) {
                            operatorvalue = document.getElementById(opname).value;
                        }
                        else {
                            operatorvalue = "=";
                        }

                        var logopname_value = '';
                        if (document.getElementById(logopname)) {
                            logopname_value = document.getElementById(logopname).value;
                        }
                        else {
                            logopname_value = "AND";
                        }

                    if (document.getElementById(controlname)) {
                        if (document.getElementById(opname)) {
                            if ((operatorvalue !== 'IS NULL') && (operatorvalue !== 'IS NOT NULL')) {
                                if (document.getElementById(controlname).value !== '') {
                                    searchfields.searchfieldsitem.push({
                                        "field": fieldname,
                                        "operator": operatorvalue,
                                        "searchvalue": document.getElementById(controlname).value,
                                        "logicaloperator": logopname_value
                                    });
                                }
                            }
                            else {
                                    searchfields.searchfieldsitem.push({
                                        "field": fieldname,
                                        "operator": operatorvalue,
                                        "searchvalue": '',
                                        "logicaloperator": logopname_value
                                    });
                            }

                        }
                        else {
                            if (document.getElementById(controlname).value !== '') {
                                searchfields.searchfieldsitem.push({
                                        "field": fieldname,
                                        "operator": operatorvalue,
                                        "searchvalue": document.getElementById(controlname).value,
                                        "logicaloperator": logopname_value
                                    });
                            }
                        }
                    }
                } 
            }

            document.getElementById("<%= hdnSearchFields.ClientID%>").value = JSON.stringify(searchfields);

        }

        function ConfirmDeleteLeadFromTable(IsDeleted) {
            if (IsDeleted==1) {
                return false;
            }
            else {
                return ConfirmDelete();
            }
        }

        function operatoronchange(obj, fieldtype) {
            var operatorname = obj.id;
            var fieldname = operatorname.substr(operatorname.indexOf("_") + 1);

            if ((obj.value == 'IS NULL') || (obj.value == 'IS NOT NULL')) {
                if (fieldtype == 'text') {
                    document.getElementById("txt" + fieldname).value = '';
                    document.getElementById("txt" + fieldname).disabled = true;
                }
                else if (fieldtype == 'select') {
                    document.getElementById("ddl" + fieldname).value = '';
                    document.getElementById("ddl" + fieldname).disabled = true;
                }
            }
            else {
                if (fieldtype == 'text') {
                    if (document.getElementById("txt" + fieldname)) {
                        document.getElementById("txt" + fieldname).disabled = false;
                    }
                }
                else if (fieldtype == 'select') {
                    if (document.getElementById("ddl" + fieldname)) {
                        document.getElementById("ddl" + fieldname).disabled = false;
                    }
                }
            }
        }

    </script>
	<link href="../dcjqaccordion-master/css/skins/graphite.css" rel="stylesheet" type="text/css" />
    <title>Marketing Automation Tool - Simplified Search</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpShortCut" runat="server"> 
    <uc:NewEmailCampaign runat="server" id="NewEmailCampaign"></uc:NewEmailCampaign>
</asp:Content>  
<asp:Content ID="Content3" ContentPlaceHolderID="cpTabHeaderBottom" runat="server"> 
    <uc:LeadTopMenu runat="server" id="ucLeadTopMenu"></uc:LeadTopMenu>
</asp:Content>  
<asp:Content ID="Content4" ContentPlaceHolderID="cpRowCrumbs" runat="server"> 
    <div class="container-fluid">
				<nav class="breadcrumb">
                  <asp:LinkButton ID="lnkBreadHome" runat="server" CssClass="breadcrumb-item" OnClick="lnkBreadHome_Click" Text="Home"></asp:LinkButton>
                  <asp:LinkButton ID="lnkLeads" runat="server" CssClass="breadcrumb-item"  Text="Leads" OnClick="lnkLeads_Click"></asp:LinkButton>
				  <span class="breadcrumb-item active">Simplified Search</span>
				</nav>
			</div>
</asp:Content>  
<asp:Content ID="Content5" ContentPlaceHolderID="cpBody" runat="server"> 
     <uc:LeftSearch runat="server" id="LeadLeftSearch"></uc:LeftSearch>
				<div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 middlecontent">
					<div class="page_header">
						<h5 class="page_title"><asp:Label ID="lblPageTitle" runat="server" Text=""></asp:Label></h5>
					</div>
					<div class="page_content">
						<div class="container-fluid">
                            <div class="" role="alert" id="dvMessage" runat="server" style="display:none;">
	                              <button type="button" class="close" data-dismiss="alert" aria-label="Close">
        	                            <span aria-hidden="true">&times;</span>
                                   </button>
                             </div>
							<ul class="nav nav-tabs" id="secondtab">
							  <li class="nav-item">
								<a class="nav-link active" href="#" id="lnkSearch" >Simplified Search</a>
							  </li>
							  <li class="nav-item">
								<a class="nav-link" href="#"  id="lnkResults">Search Results</a>
							  </li>
							</ul>
							<div class="tab-content">
							<div class="tab-pane active" id="tab_Search">
									<div class="form-horizontal">
										<div class="container">
										<div class="row">
												<div class="form-group form-group-sm col-sm-12">
												<div class="accordion" id="simplifiedSearch">
											  <div class="card">
												<div class="card-header" id="headingOne">
												  <h2 class="mb-0">
													<button class="btn btn-link" type="button" data-toggle="collapse" data-target="#collapseOne" aria-expanded="true" aria-controls="collapseOne">
													  Built-in Fields
													</button>
												  </h2>
												</div>

												<div id="collapseOne" class="collapse show" aria-labelledby="headingOne" data-parent="#simplifiedSearch">
												  <div class="card-body" id="BuiltInFields">
													<div class="row">
															<div class="form-group form-group-sm col-sm-12" id="dvgroupfirstname">
																<div class="row">
																	<div class="col-sm-3 text-right">
																		<label class="col-form-label">First Name</label>
																	</div>
																	<div class="col-sm-2">
																		<select  id="operator_firstname" class="form-control" onchange="operatoronchange(this,'text')">
																			<option value="LIKE">LIKE</option>
                                                                            <option value="NOT LIKE">NOT LIKE</option>
                                                                            <option value="=">=</option>
                                                                            <option value="<>"><></option>
                                                                            <option value="IS NULL">IS NULL</option>
                                                                            <option value="IS NOT NULL">IS NOT NULL</option>
                                                                            <option value="<"><</option>
                                                                            <option value="<="><=</option>
                                                                            <option value=">">></option>
                                                                            <option value=">=">>=</option>
																		</select>
																	</div>
																	<div class="col-sm-4">
																		<input  type="text" id="txtfirstname" class="form-control" maxlength="100"/>																
																	</div>
																	<div class="col-sm-2">
																		<select id="logicaloperator_firstname" class="form-control">
																			<option>AND</option>
																			<option>OR</option>
																		</select>
																	</div>
																	<div class="col-sm-1 pull-left">
																		<a href="#dvgroupfirstname" class="btn btn-secondary" onclick="AddField('firstname')"><i class="fas fa-plus-circle"></i></a>
																	</div>
																</div>
                                                                <div id="dv_addl_firstname">

                                                                    
                                                                </div>
															</div>
														</div>
                                                      <div class="row">
															<div class="form-group form-group-sm col-sm-12" id="dvgroupmiddlename">
																<div class="row">
																	<div class="col-sm-3 text-right">
																		<label class="col-form-label">Middle Name</label>
																	</div>
																	<div class="col-sm-2">
																		<select  id="operator_middlename" class="form-control" onchange="operatoronchange(this,'text')">
																			<option value="LIKE">LIKE</option>
                                                                            <option value="NOT LIKE">NOT LIKE</option>
                                                                            <option value="=">=</option>
                                                                            <option value="<>"><></option>
                                                                            <option value="IS NULL">IS NULL</option>
                                                                            <option value="IS NOT NULL">IS NOT NULL</option>
                                                                            <option value="<"><</option>
                                                                            <option value="<="><=</option>
                                                                            <option value=">">></option>
                                                                            <option value=">=">>=</option>
																		</select>
																	</div>
																	<div class="col-sm-4">
																		<input  type="text" id="txtmiddlename" class="form-control" maxlength="100"/>																
																	</div>
																	<div class="col-sm-2">
																		<select id="logicaloperator_middlename" class="form-control">
																			<option>AND</option>
																			<option>OR</option>
																		</select>
																	</div>
																	<div class="col-sm-1 pull-left">
																		<a href="#dvgroupmiddlename" class="btn btn-secondary" onclick="AddField('middlename')"><i class="fas fa-plus-circle"></i></a>
																	</div>
																</div>
                                                                <div id="dv_addl_middlename">

                                                                    
                                                                </div>
															</div>
														</div>
														<div class="row">
															<div class="form-group form-group-sm col-sm-12" id="dvgrouplastname">
																<div class="row">
																	<div class="col-sm-3 text-right">
																		<label class="col-form-label">Last Name</label>
																	</div>
																	<div class="col-sm-2">
																		<select  id="operator_lastname" class="form-control" onchange="operatoronchange(this,'text')">
																			<option value="LIKE">LIKE</option>
                                                                            <option value="NOT LIKE">NOT LIKE</option>
                                                                            <option value="=">=</option>
                                                                            <option value="<>"><></option>
                                                                            <option value="IS NULL">IS NULL</option>
                                                                            <option value="IS NOT NULL">IS NOT NULL</option>
                                                                            <option value="<"><</option>
                                                                            <option value="<="><=</option>
                                                                            <option value=">">></option>
                                                                            <option value=">=">>=</option>
																		</select>
																	</div>
																	<div class="col-sm-4">
																		<input  type="text" id="txtlastname" class="form-control" maxlength="100"/>																
																	</div>
																	<div class="col-sm-2">
																		<select id="logicaloperator_lastname" class="form-control">
																			<option>AND</option>
																			<option>OR</option>
																		</select>
																	</div>
																	<div class="col-sm-1 pull-left">
																		<a href="#dvgrouplastname" class="btn btn-secondary" onclick="AddField('lastname')"><i class="fas fa-plus-circle"></i></a>
																	</div>
																</div>
                                                                <div id="dv_addl_lastname">

                                                                    
                                                                </div>
															</div>
														</div>
														<div class="row">
															<div class="form-group form-group-sm col-sm-12" id="dvgroupemail">
																<div class="row">
																	<div class="col-sm-3 text-right">
																		<label class="col-form-label">Email</label>
																	</div>
																	<div class="col-sm-2">
																		<select  id="operator_email" class="form-control" onchange="operatoronchange(this,'text')">
																			<option value="LIKE">LIKE</option>
                                                                            <option value="NOT LIKE">NOT LIKE</option>
                                                                            <option value="=">=</option>
                                                                            <option value="<>"><></option>
                                                                            <option value="IS NULL">IS NULL</option>
                                                                            <option value="IS NOT NULL">IS NOT NULL</option>
                                                                            <option value="<"><</option>
                                                                            <option value="<="><=</option>
                                                                            <option value=">">></option>
                                                                            <option value=">=">>=</option>
																		</select>
																	</div>
																	<div class="col-sm-4">
																		<input  type="text" id="txtemail" class="form-control" maxlength="250"/>																
																	</div>
																	<div class="col-sm-2">
																		<select id="logicaloperator_email" class="form-control">
																			<option>AND</option>
																			<option>OR</option>
																		</select>
																	</div>
																	<div class="col-sm-1 pull-left">
																		<a href="#dvgroupemail" class="btn btn-secondary" onclick="AddField('email')"><i class="fas fa-plus-circle"></i></a>
																	</div>
																</div>
                                                                <div id="dv_addl_email">

                                                                    
                                                                </div>
															</div>
														</div>
														<div class="row">
															<div class="form-group form-group-sm col-sm-12" id="dvgroupcompanyname">
																<div class="row">
																	<div class="col-sm-3 text-right">
																		<label class="col-form-label">Company Name</label>
																	</div>
																	<div class="col-sm-2">
																		<select  id="operator_companyname" class="form-control" onchange="operatoronchange(this,'text')">
																			<option value="LIKE">LIKE</option>
                                                                            <option value="NOT LIKE">NOT LIKE</option>
                                                                            <option value="=">=</option>
                                                                            <option value="<>"><></option>
                                                                            <option value="IS NULL">IS NULL</option>
                                                                            <option value="IS NOT NULL">IS NOT NULL</option>
                                                                            <option value="<"><</option>
                                                                            <option value="<="><=</option>
                                                                            <option value=">">></option>
                                                                            <option value=">=">>=</option>
																		</select>
																	</div>
																	<div class="col-sm-4">
																		<input  type="text" id="txtcompanyname" class="form-control" maxlength="100"/>																
																	</div>
																	<div class="col-sm-2">
																		<select id="logicaloperator_companyname" class="form-control">
																			<option>AND</option>
																			<option>OR</option>
																		</select>
																	</div>
																	<div class="col-sm-1 pull-left">
																		<a href="#dvgroupcompanyname" class="btn btn-secondary" onclick="AddField('companyname')"><i class="fas fa-plus-circle"></i></a>
																	</div>
																</div>
                                                                <div id="dv_addl_companyname">

                                                                    
                                                                </div>
															</div>
														</div>
														<div class="row">
															<div class="form-group form-group-sm col-sm-12" id="dvgroupposition">
																<div class="row">
																	<div class="col-sm-3 text-right">
																		<label class="col-form-label">Position</label>
																	</div>
																	<div class="col-sm-2">
																		<select  id="operator_position" class="form-control" onchange="operatoronchange(this,'text')">
																			<option value="LIKE">LIKE</option>
                                                                            <option value="NOT LIKE">NOT LIKE</option>
                                                                            <option value="=">=</option>
                                                                            <option value="<>"><></option>
                                                                            <option value="IS NULL">IS NULL</option>
                                                                            <option value="IS NOT NULL">IS NOT NULL</option>
                                                                            <option value="<"><</option>
                                                                            <option value="<="><=</option>
                                                                            <option value=">">></option>
                                                                            <option value=">=">>=</option>
																		</select>
																	</div>
																	<div class="col-sm-4">
																		<input  type="text" id="txtposition" class="form-control" maxlength="50"/>																
																	</div>
																	<div class="col-sm-2">
																		<select id="logicaloperator_position" class="form-control">
																			<option>AND</option>
																			<option>OR</option>
																		</select>
																	</div>
																	<div class="col-sm-1 pull-left">
																		<a href="#dvgroupposition" class="btn btn-secondary" onclick="AddField('position')"><i class="fas fa-plus-circle"></i></a>
																	</div>
																</div>
                                                                <div id="dv_addl_position">

                                                                    
                                                                </div>
															</div>
														</div>
														<div class="row">
															<div class="form-group form-group-sm col-sm-12" id="dvgroupwebsite" >
																<div class="row">
																	<div class="col-sm-3 text-right">
																		<label class="col-form-label">Website</label>
																	</div>
																	<div class="col-sm-2">
																		<select  id="operator_website" class="form-control" onchange="operatoronchange(this,'text')">
																			<option value="LIKE">LIKE</option>
                                                                            <option value="NOT LIKE">NOT LIKE</option>
                                                                            <option value="=">=</option>
                                                                            <option value="<>"><></option>
                                                                            <option value="IS NULL">IS NULL</option>
                                                                            <option value="IS NOT NULL">IS NOT NULL</option>
                                                                            <option value="<"><</option>
                                                                            <option value="<="><=</option>
                                                                            <option value=">">></option>
                                                                            <option value=">=">>=</option>
																		</select>
																	</div>
																	<div class="col-sm-4">
																		<input  type="text" id="txtwebsite" class="form-control" maxlength="250"/>																
																	</div>
																	<div class="col-sm-2">
																		<select id="logicaloperator_website" class="form-control">
																			<option>AND</option>
																			<option>OR</option>
																		</select>
																	</div>
																	<div class="col-sm-1 pull-left">
																		<a href="#dvgroupwebsite" class="btn btn-secondary" onclick="AddField('website')"><i class="fas fa-plus-circle"></i></a>
																	</div>
																</div>
                                                                <div id="dv_addl_website">

                                                                    
                                                                </div>
															</div>
														</div>
														<div class="row">
															<div class="form-group form-group-sm col-sm-12" id="dvgroupmobile">
																<div class="row">
																	<div class="col-sm-3 text-right">
																		<label class="col-form-label">Mobile</label>
																	</div>
																	<div class="col-sm-2">
																		<select  id="operator_mobile" class="form-control" onchange="operatoronchange(this,'text')">
																			<option value="LIKE">LIKE</option>
                                                                            <option value="NOT LIKE">NOT LIKE</option>
                                                                            <option value="=">=</option>
                                                                            <option value="<>"><></option>
                                                                            <option value="IS NULL">IS NULL</option>
                                                                            <option value="IS NOT NULL">IS NOT NULL</option>
                                                                            <option value="<"><</option>
                                                                            <option value="<="><=</option>
                                                                            <option value=">">></option>
                                                                            <option value=">=">>=</option>
																		</select>
																	</div>
																	<div class="col-sm-4">
																		<input  type="text" id="txtmobile" class="form-control" maxlength="100"/>																
																	</div>
																	<div class="col-sm-2">
																		<select id="logicaloperator_mobile" class="form-control">
																			<option>AND</option>
																			<option>OR</option>
																		</select>
																	</div>
																	<div class="col-sm-1 pull-left">
																		<a href="#dvgroupmobile" class="btn btn-secondary" onclick="AddField('mobile')"><i class="fas fa-plus-circle"></i></a>
																	</div>
																</div>
                                                                <div id="dv_addl_mobile">

                                                                    
                                                                </div>
															</div>
														</div>
														<div class="row">
															<div class="form-group form-group-sm col-sm-12" id="dvgroupphoneno">
																<div class="row">
																	<div class="col-sm-3 text-right">
																		<label class="col-form-label">Phone No</label>
																	</div>
																	<div class="col-sm-2">
																		<select  id="operator_phoneno" class="form-control" onchange="operatoronchange(this,'text')">
																			<option value="LIKE">LIKE</option>
                                                                            <option value="NOT LIKE">NOT LIKE</option>
                                                                            <option value="=">=</option>
                                                                            <option value="<>"><></option>
                                                                            <option value="IS NULL">IS NULL</option>
                                                                            <option value="IS NOT NULL">IS NOT NULL</option>
                                                                            <option value="<"><</option>
                                                                            <option value="<="><=</option>
                                                                            <option value=">">></option>
                                                                            <option value=">=">>=</option>
																		</select>
																	</div>
																	<div class="col-sm-4">
																		<input  type="text" id="txtphoneno" class="form-control" maxlength="100"/>																
																	</div>
																	<div class="col-sm-2">
																		<select id="logicaloperator_phoneno" class="form-control">
																			<option>AND</option>
																			<option>OR</option>
																		</select>
																	</div>
																	<div class="col-sm-1 pull-left">
																		<a href="#dvgroupphoneno" class="btn btn-secondary" onclick="AddField('phoneno')"><i class="fas fa-plus-circle"></i></a>
																	</div>
																</div>
                                                                <div id="dv_addl_phoneno">

                                                                    
                                                                </div>
															</div>
														</div>
														<div class="row">
															<div class="form-group form-group-sm col-sm-12" id="dvgroupaddress">
																<div class="row">
																	<div class="col-sm-3 text-right">
																		<label class="col-form-label">Address</label>
																	</div>
																	<div class="col-sm-2">
																		<select  id="operator_address" class="form-control" onchange="operatoronchange(this,'text')">
																			<option value="LIKE">LIKE</option>
                                                                            <option value="NOT LIKE">NOT LIKE</option>
                                                                            <option value="=">=</option>
                                                                            <option value="<>"><></option>
                                                                            <option value="IS NULL">IS NULL</option>
                                                                            <option value="IS NOT NULL">IS NOT NULL</option>
                                                                            <option value="<"><</option>
                                                                            <option value="<="><=</option>
                                                                            <option value=">">></option>
                                                                            <option value=">=">>=</option>
																		</select>
																	</div>
																	<div class="col-sm-4">
																		<input  type="text" id="txtaddress" class="form-control" maxlength="501"/>																
																	</div>
																	<div class="col-sm-2">
																		<select id="logicaloperator_address" class="form-control">
																			<option>AND</option>
																			<option>OR</option>
																		</select>
																	</div>
																	<div class="col-sm-1 pull-left">
																		<a href="#dvgroupaddress" class="btn btn-secondary" onclick="AddField('address')"><i class="fas fa-plus-circle"></i></a>
																	</div>
																</div>
                                                                <div id="dv_addl_address">

                                                                    
                                                                </div>
															</div>
														</div>
														<div class="row">
															<div class="form-group form-group-sm col-sm-12" id="dvgroupcity">
																<div class="row">
																	<div class="col-sm-3 text-right">
																		<label class="col-form-label">City</label>
																	</div>
																	<div class="col-sm-2">
																		<select  id="operator_city" class="form-control" onchange="operatoronchange(this,'text')">
																			<option value="LIKE">LIKE</option>
                                                                            <option value="NOT LIKE">NOT LIKE</option>
                                                                            <option value="=">=</option>
                                                                            <option value="<>"><></option>
                                                                            <option value="IS NULL">IS NULL</option>
                                                                            <option value="IS NOT NULL">IS NOT NULL</option>
                                                                            <option value="<"><</option>
                                                                            <option value="<="><=</option>
                                                                            <option value=">">></option>
                                                                            <option value=">=">>=</option>
																		</select>
																	</div>
																	<div class="col-sm-4">
																		<input  type="text" id="txtcity" class="form-control" maxlength="50"/>																
																	</div>
																	<div class="col-sm-2">
																		<select id="logicaloperator_city" class="form-control">
																			<option>AND</option>
																			<option>OR</option>
																		</select>
																	</div>
																	<div class="col-sm-1 pull-left">
																		<a href="#dvgroupcity" class="btn btn-secondary" onclick="AddField('city')"><i class="fas fa-plus-circle"></i></a>
																	</div>
																</div>
                                                                <div id="dv_addl_city">

                                                                    
                                                                </div>
															</div>
														</div>
														<div class="row">
															<div class="form-group form-group-sm col-sm-12" id="dvgroupstate">
																<div class="row">
																	<div class="col-sm-3 text-right">
																		<label class="col-form-label">State</label>
																	</div>
																	<div class="col-sm-2">
																		<select  id="operator_state" class="form-control" onchange="operatoronchange(this,'text')">
																			<option value="LIKE">LIKE</option>
                                                                            <option value="NOT LIKE">NOT LIKE</option>
                                                                            <option value="=">=</option>
                                                                            <option value="<>"><></option>
                                                                            <option value="IS NULL">IS NULL</option>
                                                                            <option value="IS NOT NULL">IS NOT NULL</option>
                                                                            <option value="<"><</option>
                                                                            <option value="<="><=</option>
                                                                            <option value=">">></option>
                                                                            <option value=">=">>=</option>
																		</select>
																	</div>
																	<div class="col-sm-4">
																		<input  type="text" id="txtstate" class="form-control" maxlength="50"/>																
																	</div>
																	<div class="col-sm-2">
																		<select id="logicaloperator_state" class="form-control">
																			<option>AND</option>
																			<option>OR</option>
																		</select>
																	</div>
																	<div class="col-sm-1 pull-left">
																		<a href="#dvgroupstate" class="btn btn-secondary" onclick="AddField('state')"><i class="fas fa-plus-circle"></i></a>
																	</div>
																</div>
                                                                <div id="dv_addl_state">

                                                                    
                                                                </div>
															</div>
														</div>
														<div class="row">
															<div class="form-group form-group-sm col-sm-12" id="dvgroupcountry">
																<div class="row">
																	<div class="col-sm-3 text-right">
																		<label class="col-form-label">Country</label>
																	</div>
																	<div class="col-sm-2">
																		<select  id="operator_country" class="form-control" onchange="operatoronchange(this,'select')">
																			<option value="LIKE">LIKE</option>
                                                                            <option value="NOT LIKE">NOT LIKE</option>
                                                                            <option value="=">=</option>
                                                                            <option value="<>"><></option>
                                                                            <option value="IS NULL">IS NULL</option>
                                                                            <option value="IS NOT NULL">IS NOT NULL</option>
                                                                            <option value="<"><</option>
                                                                            <option value="<="><=</option>
                                                                            <option value=">">></option>
                                                                            <option value=">=">>=</option>
																		</select>
																	</div>
																	<div class="col-sm-4">
																		<select id="ddlcountry" class="form-control">
																			
																		</select>															
																	</div>
																	<div class="col-sm-2">
																		<select id="logicaloperator_country" class="form-control">
																			<option>AND</option>
																			<option>OR</option>
																		</select>
																	</div>
																	<div class="col-sm-1 pull-left">
																		<a href="#dvgroupcountry" class="btn btn-secondary" onclick="AddField('country')"><i class="fas fa-plus-circle"></i></a>
																	</div>
																</div>
                                                                <div id="dv_addl_country">

                                                                    
                                                                </div>
															</div>
														</div>
                                                      <div class="row">
															<div class="form-group form-group-sm col-sm-12">
																<div class="row">
																	<div class="col-sm-3 text-right">
																		<label class="col-form-label">Type</label>
																	</div>
																	<div class="col-sm-2">
																		<select  id="operator_contacttype" class="form-control" onchange="operatoronchange(this,'select')">
																			<option value="LIKE">LIKE</option>
                                                                            <option value="NOT LIKE">NOT LIKE</option>
                                                                            <option value="=">=</option>
                                                                            <option value="<>"><></option>
                                                                            <option value="IS NULL">IS NULL</option>
                                                                            <option value="IS NOT NULL">IS NOT NULL</option>
                                                                            <option value="<"><</option>
                                                                            <option value="<="><=</option>
                                                                            <option value=">">></option>
                                                                            <option value=">=">>=</option>
																		</select>
																	</div>
																	<div class="col-sm-4">
																		<select id="ddlcontacttype" class="form-control">
                                                                            <option value=""></option>
																			<option value="1">Lead</option>
																			<option value="2">Contact</option>
																		</select>															
																	</div>
                                                                    <div class="col-sm-2">
																		<select id="logicaloperator_contacttype" class="form-control">
																			<option>AND</option>
																			<option>OR</option>
																		</select>
																	</div>
																</div>
															</div>
														</div>
														<div class="row">
															<div class="form-group form-group-sm col-sm-12" id="dvgroupcontactstatus">
																<div class="row">
																	<div class="col-sm-3 text-right">
																		<label class="col-form-label">Status</label>
																	</div>
																	<div class="col-sm-2">
																		<select  id="operator_contactstatus" class="form-control" onchange="operatoronchange(this,'select')">
																			<option value="LIKE">LIKE</option>
                                                                            <option value="NOT LIKE">NOT LIKE</option>
                                                                            <option value="=">=</option>
                                                                            <option value="<>"><></option>
                                                                            <option value="IS NULL">IS NULL</option>
                                                                            <option value="IS NOT NULL">IS NOT NULL</option>
                                                                            <option value="<"><</option>
                                                                            <option value="<="><=</option>
                                                                            <option value=">">></option>
                                                                            <option value=">=">>=</option>
																		</select>
																	</div>
																	<div class="col-sm-4">
																		<select id="ddlcontactstatus" class="form-control">
                                                                            <option value="" ></option>
                                                                            <option value="1">Not Confirmed</option>
																			<option value="2">Confirmed</option>
																			<option value="3">Active</option>
																			<option value="4">Inactive</option>
																		</select>															
																	</div>
																	<div class="col-sm-2">
																		<select id="logicaloperator_contactstatus" class="form-control">
																			<option>AND</option>
																			<option>OR</option>
																		</select>
																	</div>
																	<div class="col-sm-1 pull-left">
																		<a href="#dvgroupcontactstatus" class="btn btn-secondary" onclick="AddField('contactstatus')"><i class="fas fa-plus-circle"></i></a>
																	</div>
																</div>
                                                                <div id="dv_addl_contactstatus">

                                                                    
                                                                </div>
															</div>
														</div>
														<div class="row">
															<div class="form-group form-group-sm col-sm-12">
																<div class="row">
																	<div class="col-sm-3 text-right">
																		<label class="col-form-label">Gender</label>
																	</div>
																	<div class="col-sm-2">
																		<select  id="operator_gender" class="form-control" onchange="operatoronchange(this,'select')">
																			<option value="LIKE">LIKE</option>
                                                                            <option value="NOT LIKE">NOT LIKE</option>
                                                                            <option value="=">=</option>
                                                                            <option value="<>"><></option>
                                                                            <option value="IS NULL">IS NULL</option>
                                                                            <option value="IS NOT NULL">IS NOT NULL</option>
                                                                            <option value="<"><</option>
                                                                            <option value="<="><=</option>
                                                                            <option value=">">></option>
                                                                            <option value=">=">>=</option>
																		</select>
																	</div>
																	<div class="col-sm-4">
																		<select id="ddlgender" class="form-control">
																			<option></option>
																			<option>Male</option>
																			<option>Female</option>
																		</select>															
																	</div>
                                                                    <div class="col-sm-2">
																		<select id="logicaloperator_gender" class="form-control">
																			<option>AND</option>
																			<option>OR</option>
																		</select>
																	</div>
																</div>
															</div>
														</div>
														<div class="row">
															<div class="form-group form-group-sm col-sm-12">
																<div class="row">
																	<div class="col-sm-3 text-right">
																		<label class="col-form-label">Email Subscription</label>
																	</div>
																	<div class="col-sm-6">
																		<select id="ddlsubscribedtoemail" class="form-control" >
																				<option value=""></option>
																				<option value="0">Unsubscribed</option>
																				<option value="1">Subscribed</option>
																			</select>														
																	</div>
                                                                    <div class="col-sm-2">
																		<select id="logicaloperator_subscribedtoemail" class="form-control">
																			<option>AND</option>
																			<option>OR</option>
																		</select>
																	</div>
																</div>
															</div>
														</div>
                                                      <div class="row">
															<div class="form-group form-group-sm col-sm-12">
																<div class="row">
																	<div class="col-sm-3 text-right">
																		<label class="col-form-label">Deleted</label>
																	</div>
																	<div class="col-sm-6">
																		<select id="ddlisdeleted" class="form-control">
																				<option value=""></option>
																				<option value="1">Yes</option>
																				<option value="0" selected>No</option>
																	    </select>														
																	</div>
                                                                    <div class="col-sm-2">
																		<select id="logicaloperator_isdeleted" class="form-control">
																			<option>AND</option>
																			<option>OR</option>
																		</select>
																	</div>
																</div>
															</div>
														</div>
                                                      <div class="row">
															<div class="form-group form-group-sm col-sm-12">
																<div class="row">
																	<div class="col-sm-3 text-right">
																		<label class="col-form-label">Used for Testing</label>
																	</div>
																	<div class="col-sm-6">
																		<select id="ddlusefortesting" class="form-control">
																				<option value=""></option>
																				<option value="1">Yes</option>
																				<option value="0">No</option>
																			</select>														
																	</div>
                                                                    <div class="col-sm-2">
																		<select id="logicaloperator_usefortesting" class="form-control">
																			<option>AND</option>
																			<option>OR</option>
																		</select>
																	</div>
																</div>
															</div>
														</div>
												  </div>
												</div>
											  </div>
											  <div class="card">
												<div class="card-header" id="headingLists">
												  <h2 class="mb-0">
													<button class="btn btn-link collapsed" type="button" data-toggle="collapse" data-target="#collapseLists" aria-expanded="false" aria-controls="collapseThree">
													  Lists
													</button>
												  </h2>
												</div>
												<div id="collapseLists" class="collapse" aria-labelledby="headingLists" data-parent="#simplifiedSearch">
												  <div class="card-body">
													<div class="form-horizontal">
														<div class="container">
															<div class="row">
																<div class="form-group form-group-sm col-sm-12" id="dvgroupcontactlist">
																	<div class="row">
																		<div class="col-sm-2 text-right">
																			<label class="col-form-label" >Select a List</label>
																		</div>
																		<div class="col-sm-7">
																			 <select id="ddlcontactlist"  class="form-control"></select>
																		</div>
																		<div class="col-sm-2">
																			  <select id="logicaloperator_contactlist" class="form-control">
																				<option>AND</option>
																				<option>OR</option>
																			</select>
																		</div>
																		<div class="col-sm-1 pull-left">
																			  <a href="#dvgroupcontactlist" class="btn btn-secondary" onclick="AddField('contactlist')"><i class="fas fa-plus-circle"></i></a>
																		</div>
																	</div>
                                                                     <div id="dv_addl_contactlist">

                                                                    
                                                                    </div>
																</div>
															</div>
														</div>
													</div>
												  </div>
												</div>
											  </div>
											</div>
												</div>
										</div>
											<div class="row">
												<div class="form-group form-group-sm col-sm-12">
													<div class="row">
													<div class="col-sm-12 button_panel">
														<div class="float-right mt-2">
															<asp:Button ID="btnSearch" runat="server" Text="Search" class="btn btn-primary" OnClientClick="return GetSearchValues();" OnClick="btnSearch_Click" />
														</div>
													</div>
													</div>
												</div>
											</div>
											
										</div>
									</div>
								</div>
								<div class="tab-pane" id="tab_Results">
									<div class="form-horizontal">
										<div class="container">
											<div class="row">
												<div class="col-lg-12">
													<div class="alert alert-success searchcriteria" role="alert" runat="server" id="dvSearchCriteria">
													 
													</div>
												</div>
											</div>
                                            <div runat="server" id="dvIncludedLabel">
                                            <legend class="legendstyle">Included</legend>
                                            </div>
											<div class="row">
												<div class="col-lg-12">
                                                    <asp:GridView ID="grdIncluded" runat="server" AutoGenerateColumns="False" CssClass="table table-striped" GridLines="None" EmptyDataText="No Data Available" ShowHeaderWhenEmpty="True" OnRowCommand="grdIncluded_RowCommand" OnRowDeleting="grdIncluded_RowDeleting" OnRowDataBound="grdIncluded_RowDataBound"  >
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkExclude" runat="server" Text="EXCLUDE" CommandName="EXCLUDE" CommandArgument='<%# Bind("ID") %>'></asp:LinkButton>
                                                                </ItemTemplate>
                                                                <ItemStyle />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Lead ID">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkLeadID" runat="server" Text='<%# Bind("ID") %>' CommandName="SELECT" CommandArgument='<%# Bind("ContactID") %>'></asp:LinkButton>
                                                                </ItemTemplate>
                                                                <ItemStyle />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="FirstName">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFirstName" runat="server" Text='<%# Bind("FirstName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle />
                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="LastName">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblLastName" runat="server" Text='<%# Bind("LastName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Mobile">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMobile" runat="server" Text='<%# Bind("MobileNumber") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Email Address">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblEmailAddress" runat="server" Text='<%# Bind("EmailAddress") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Facebook ID">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFacebookID" runat="server" Text='<%# Bind("FacebookID") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkDelete"  runat="server"  CommandName="DELETE" CommandArgument='<%# Bind("ContactID") %>' OnClientClick='<%# "return ConfirmDeleteLeadFromTable(" + Eval("IsDeletedNum")  + ");" %>' rel="tooltip" class="iconlink" data-placement="top" title="Delete"><i class="fas fa-trash-alt" id="trashicon"></i></asp:LinkButton>
                                                                    <a href="#" runat="server" id="lnkNoDelete" rel="tooltip" class="iconlink" data-placement="top" title="Deleted Record"><i class="fas fa-trash-alt" id="trashicondisabled" style="color:#CCC;"></i></a>
                                                                    <asp:HiddenField ID="hdnDeleted" runat="server" Value='<%# Bind("IsDeleted") %>' />                                                               
                                                                </ItemTemplate>
                                                                <ItemStyle />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <EmptyDataRowStyle HorizontalAlign="Center" />
                                                    </asp:GridView>
                                                </div>
											</div>
											<br/>
											<div class="row" > 
												 <uc:Pager runat="server" id="Pager"></uc:Pager>
											</div>
                                            <div class="row" runat="server" id="dvExport">
												<div class="form-group form-group-sm col-sm-12">
													<div class="row">
													<div class="col-sm-12 button_panel">
														<div class="float-right mt-2">
															<asp:Button ID="btnExport" runat="server" Text="Export" class="btn btn-primary" OnClick="btnExport_Click"   />
														</div>
													</div>
													</div>
												</div>
											</div>
                                            <div class="row" runat="server" id="dvAddMembers">
												<div class="form-group form-group-sm col-sm-12">
													<div class="row">
													<div class="col-sm-12 button_panel">
														<div class="float-right mt-2">
															<asp:Button ID="btnAddMembers" runat="server" Text="Add Members" class="btn btn-primary" OnClick="btnAddMembers_Click"  />
														</div>
													</div>
													</div>
												</div>
											</div>
                                            <br/>
                                            <div runat="server" id="dvExcluded">
                                            <div runat="server" id="dvExcludedLabel">
                                            <legend class="legendstyle">Excluded</legend>
                                            </div>
											<div class="row">
												<div class="col-lg-12">
                                                    <asp:GridView ID="grdExcluded" runat="server" AutoGenerateColumns="False" CssClass="table table-striped" GridLines="None" EmptyDataText="No Data Available" ShowHeaderWhenEmpty="True" OnRowCommand="grdExcluded_RowCommand"   >
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkInclude" runat="server" Text="INCLUDE" CommandName="INCLUDE" CommandArgument='<%# Bind("ID") %>'></asp:LinkButton>
                                                                </ItemTemplate>
                                                                <ItemStyle />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Lead ID">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkLeadID" runat="server" Text='<%# Bind("ID") %>' CommandName="SELECT" CommandArgument='<%# Bind("ContactID") %>'></asp:LinkButton>
                                                                </ItemTemplate>
                                                                <ItemStyle />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="FirstName">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFirstName" runat="server" Text='<%# Bind("FirstName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle />
                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="LastName">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblLastName" runat="server" Text='<%# Bind("LastName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Mobile">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMobile" runat="server" Text='<%# Bind("MobileNumber") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Email Address">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblEmailAddress" runat="server" Text='<%# Bind("EmailAddress") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Facebook ID">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFacebookID" runat="server" Text='<%# Bind("FacebookID") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <EmptyDataRowStyle HorizontalAlign="Center" />
                                                    </asp:GridView>
                                                </div>
											</div>
											<br/>
											<div class="row" > 
												 <uc:Pager runat="server" id="PagerExcluded"></uc:Pager>
											</div>
                                                
                                            </div>
                                            
										</div>
									</div>
								</div>
							</div>
						</div>
					</div>
				</div>
				<div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 rightmenu">
				<div class="container-fluid">
					<div class="graphite accordion-container-right">
						<ul class="accordion" id="accordion-right">
							<uc:Shortcuts runat="server" id="Shortcuts"></uc:Shortcuts>
							<uc:ViewRecentSearches runat="server" id="ViewRecentSearches"></uc:ViewRecentSearches>
							<uc:LeadLists runat="server" id="LeadLists"></uc:LeadLists>
						</ul>
					</div>
				</div>	
				</div>
        <div id="dvLoading">
        </div>
        <div style="display: none;" class="ui-widget-overlay overlayforaddNote">
               <span class="note_exporting">Exporting... Please wait..</span>
        </div>

    <input type="hidden" id="hdnFirstNameIndex"/>
    <input type="hidden" id="hdnMiddleNameIndex"/>
    <input type="hidden" id="hdnLastNameIndex"/>
    <input type="hidden" id="hdnEmailIndex"/>
    <input type="hidden" id="hdnCompanyNameIndex"/>
    <input type="hidden" id="hdnPositionIndex"/>
    <input type="hidden" id="hdnWebsiteIndex"/>
    <input type="hidden" id="hdnMobileIndex"/>
    <input type="hidden" id="hdnPhoneNoIndex"/>
    <input type="hidden" id="hdnAddressIndex"/>
    <input type="hidden" id="hdnCityIndex"/>
    <input type="hidden" id="hdnStateIndex"/>
    <input type="hidden" id="hdnCountryIndex"/>
    <input type="hidden" id="hdnStatusIndex"/>
    <input type="hidden" id="hdnContactListIndex"/>
    <input type="hidden" id="hdnSearchFields" name="hdnSearchFields" runat="server"/>
    <input type="hidden" id="hdnCountryList" name="hdnCountryList" runat="server"/>
    <input type="hidden" id="hdnContactListList" name="hdnContactListList" runat="server"/>
    <input type="hidden" id="hdnActiveTab" name="hdnActiveTab" runat="server"/>
</asp:Content>  



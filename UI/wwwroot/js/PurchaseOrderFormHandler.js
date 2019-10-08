//**************************************************************************************
//  Handler for Purchase Order Views 
//**************************************************************************************
//  Global values
//**************************************************************************************
//**************************************************************************************
// Page Events buttons pressed selects changed
//**************************************************************************************
function ProcessSelection(element) {
    // processes the result of a HTML switch element change. 
    // get value from the switch.
    //alert("element is :" + element);
    var selection = document.getElementById(element + "id").value;
    //alert("Selection from " + element+"  is :" + selection);
    switch (element) {
        case "To":          
        case "DeliverTo":
        case "InvoiceTo":
            switch (selection) {
                case "NewOrganisation":
                    DisplayModalAddress(element);
                    break;
                case "Select Organisation":
                    // do nothing
                    break;
                default:
                    //GetAddressFromDatabase(element, selection);
                    if (element === "To") {
                        PopulateAddItemsToPOSelect(selection);
                        $("#ItemsList").empty();
                        document.getElementById('Totalid').value = "0.00";
                        document.getElementById('Taxid').value = "0.00";
                        document.getElementById('Priceid').value = "0.00";
                    }
                    break;
            }
            break;
        case "OrgItems":
            // check that To has been set before proceding 
            if (Tohasbeenset()) {
                switch (selection) {
                    case "NewItem":
                        DisplayModalRegisterNewItem();
                        break;
                    case "Select Item":
                        // do nothing
                        break;
                    default:
                        GetItemFromDatabase(selection);
                        break;
                }
            } else {
                alert("To needs to be set before Items can be added to the PO ");
            }
            break;
        default:
            alert("Opps " + element + " has not been defined!!!");
            break;
    }
}
function SavePOtoDatabase() {
    if (ValidateForm('PurchaseOrderFormid') === "Passed") {
        if (confirm(" Save to database")) {
            // compose JSON object to submit to database.
            var POFormData = GetPOFormData(); 
            // now send to server to be saved in the database.
            var result = PostPOtoDatabase(POFormData);         
        }
    } else {
        alert("Errors found please check");
    }
}
//**************************************************************************************
// General  Functions  
//**************************************************************************************
function ClearOptions(SelectElement) {
    // add an option to a select element
    var Selectbox = document.getElementById(SelectElement);
    for (var ct = Selectbox.options.length - 1; ct >= 0; ct--)
    {
        Selectbox.remove(ct);
    }
}
function AddOption(SelectElement, OptionText, OptionValue) {
    // add an option to a select element
    var SelectItem = document.getElementById(SelectElement);
    var NewOpt = document.createElement("option");
    NewOpt.text = OptionText;
    NewOpt.value = OptionValue;
    NewOpt.selected = false;
    SelectItem.add(NewOpt);
}
function PopulateAddItemsToPOSelect(Organisationid) {
    var url = '/PurchaseOrder/GetOrganisationItems?Organisationid=' + Organisationid;
    var xhr = new XMLHttpRequest();
    xhr.onreadystatechange = function () {
        if (this.readyState === 4 && this.status === 200) {
            if (xhr.responseText !== "Failure") {
                var result = JSON.parse(xhr.responseText);
                console.log(result);
                ClearOptions("OrgItemsid");
                AddOption("OrgItemsid", "Select Item", "");
                AddOption("OrgItemsid", "New Item", "NewItem");
                for (var i = 0; i < result.length; i++) {
                    AddOption("OrgItemsid", result[i].Code, result[i].ID);
                }
            }
            return xhr.responseText;
        }
    };
    xhr.open('get', url);
    xhr.send();
}

function TestValidateForm(FormName) {
    var ToReturn = "Passed";
    alert(" Validating form :" + FormName);
    if (ValidateForm(FormName) === "Passed") {
        alert(" Passed Validation");

    }
    else
    {
        alert(" Failed Validation");
        ToReturn = "Failed";
    }
    alert(" Validation complete ");
    // return ToReturn;
}

function ValidateForm(FormName) {
    $("form").removeData("validator").removeData("unobtrusiveValidation");
    //Parse the form again
    $.validator.unobtrusive.parse($("form"));
    var Form = $("#" + FormName);
    if (Form.valid()) {
        return "Passed";
    }
    else {
        return "Failed";
    }
}

function Tohasbeenset() {
    // the length of a Guid as a string is 36 using that to check if the TO option has been set
    var test = document.getElementById("Toid").value;
    if (test.length === 36) {
        return true;
    }
    return false;
}

function DisplayModalAddress(OpenedBy) {
    // set which select element opened the modal.
    document.getElementById("ModalNewAddressForm").setAttribute("data-OpenedBy", OpenedBy);
    // before displaying the modal ensure all the data input and error fields 
    // are blank.
    document.getElementById("ModalNewAddressOrganisationid").value = "";
    $("span[data-valmsg-for='ModalNewAddressOrganisation'").text("");
    document.getElementById("ModalNewAddressContactid").value = "";
    $("span[data-valmsg-for='ModalNewAddressContact'").text("");
    document.getElementById("ModalNewAddressEmailid").value = "";
    $("span[data-valmsg-for='ModalNewAddressEmail'").text("");
    document.getElementById("ModalNewAddressLine1id").value = "";
    $("span[data-valmsg-for='ModalNewAddressLine1'").text("");
    document.getElementById("ModalNewAddressLine2id").value = "";
    $("span[data-valmsg-for='ModalNewAddressLine2'").text("");
    document.getElementById("ModalNewAddressLine3id").value = "";
    $("span[data-valmsg-for='ModalNewAddressLine3'").text("");
    document.getElementById("ModalNewAddressLine4id").value = "";
    $("span[data-valmsg-for='ModalNewAddressLine4'").text("");
    document.getElementById("ModalNewAddressLine5id").value = "";
    $("span[data-valmsg-for='ModalNewAddressLine5'").text("");
    document.getElementById("ModalNewAddressCodeid").value = "";
    $("span[data-valmsg-for='ModalNewAddressCode'").text("");
    $("span[data-valmsg-for='ModalNewAddressAddress'").text("");
    // display the modal
    PopulateAddressModal();
    $('#ModalNewAddress').modal('show');
}
function SaveAddressModal() {

    if (ValidateForm("ModalNewAddressForm") === "Passed") {
        var SavetoDb = PostAddresstoDatabase();
    }
}


function GetModalAddressData() {
    var ToReturn = "";
    ToReturn = ToReturn + document.getElementById("ModalNewAddressOrganisationid").value + "<br />";
    ToReturn = ToReturn + document.getElementById("ModalNewAddressContactid").value + "<br />";
    ToReturn = ToReturn + document.getElementById("ModalNewAddressEmailid").value + "<br />";
    ToReturn = ToReturn + document.getElementById("ModalNewAddressLine1id").value + "<br />";
    if (document.getElementById("ModalNewAddressLine2id").value.length !== 0) {
        ToReturn = ToReturn + document.getElementById("ModalNewAddressLine2id").value + "<br />";
    }

    if (document.getElementById("ModalNewAddressLine3id").value.length !== 0) {
        ToReturn = ToReturn + document.getElementById("ModalNewAddressLine3id").value + "<br />";
    }

    if (document.getElementById("ModalNewAddressLine4id").value.length !== 0) {
        ToReturn = ToReturn + document.getElementById("ModalNewAddressLine4id").value + "<br />";
    }

    if (document.getElementById("ModalNewAddressLine5id").value.length !== 0) {
        ToReturn = ToReturn + document.getElementById("ModalNewAddressLine5id").value + "<br />";
    }
    ToReturn = ToReturn + document.getElementById("ModalNewAddressCodeid").value;
    return ToReturn;
}


function GetModalAddressDataObject() {
    // define object
    var ToReturn =
    {
        Organisation: "",
        Contact: "",
        Email: "",
        Line1: "",
        Line2: "",
        Line3: "",
        Line4: "",
        Line5: "",
        Code: ""
    }
    // fill object  
    ToReturn.Code = document.getElementById("ModalNewAddressCodeid").value;
    ToReturn.Contact = document.getElementById("ModalNewAddressContactid").value;
    ToReturn.Email = document.getElementById("ModalNewAddressEmailid").value;
    ToReturn.Line1 = document.getElementById("ModalNewAddressLine1id").value;
    ToReturn.Line2 = document.getElementById("ModalNewAddressLine2id").value;
    ToReturn.Line3 = document.getElementById("ModalNewAddressLine3id").value;
    ToReturn.Line4 = document.getElementById("ModalNewAddressLine4id").value;
    ToReturn.Line5 = document.getElementById("ModalNewAddressLine5id").value;
    ToReturn.Organisation = document.getElementById("ModalNewAddressOrganisationid").value;
    // return populated object
    return ToReturn;
}
function DisplayonView() {
    var Openedby =  document.getElementById("ModalNewAddressForm").getAttribute("data-OpenedBy") + "DetailId";
    document.getElementById(Openedby).innerHTML = GetModalAddressData();

   

}

function PopulateOrganisationDropDownLists(OptionValue) {
    var Opt1 = document.createElement("option");
    var AttributeValue = GetModalAddressData(); 

    Opt1.text = document.getElementById("ModalNewAddressOrganisationid").value;
    Opt1.value = OptionValue;
    Opt1.setAttribute("data-subtext", AttributeValue);

    Opt1.selected = false;

    var Opt2 = document.createElement("option");
    Opt2.text = document.getElementById("ModalNewAddressOrganisationid").value;
    Opt2.value = OptionValue;
    Opt2.selected = false;
    Opt2.setAttribute("data-subtext", AttributeValue);

    var Opt3 = document.createElement("option");
    Opt3.text = document.getElementById("ModalNewAddressOrganisationid").value;
    Opt3.value = OptionValue;
    Opt3.selected = false;
    Opt3.setAttribute("data-subtext", AttributeValue);

    document.getElementById("Toid").add(Opt1);
    document.getElementById("DeliverToid").add(Opt2);
    document.getElementById("InvoiceToid").add(Opt3);
  
}




function PostAddresstoDatabase() {
    var data = GetModalAddressDataObject();
    var url = '/PurchaseOrder/SaveOrganisation?Organisation=' + data.Organisation +
        '\&Contact=' + data.Contact +
        '\&Email=' + data.Email +
        '\&Line1=' + data.Line1 +
        '\&Line2=' + data.Line2 +
        '\&Line3=' + data.Line3 +
        '\&Line4=' + data.Line4 +
        '\&Line5=' + data.Line5 +
        '\&code=' + data.Code;
    var xhr = new XMLHttpRequest();
    xhr.onreadystatechange = function () {
        if (this.readyState === 4 && this.status === 200) {
            //alert("Server response is :" + xhr.responseText);
            if (xhr.responseText !== "Failure") {
                DisplayonView();
                PopulateOrganisationDropDownLists(xhr.responseText);
                document.getElementById("Toid").value = xhr.responseText;
                $('#ModalNewAddress').modal('hide');
            }
            return xhr.responseText;
        }
    };
    xhr.open('get', url);
    xhr.send();
}
function PopulateAddressModal() {
    document.getElementById("ModalNewAddressOrganisationid").value = "New Organisation";
    document.getElementById("ModalNewAddressContactid").value = " New Contact";
    document.getElementById("ModalNewAddressEmailid").value = "Email@mail.com ";
    document.getElementById("ModalNewAddressLine1id").value = "Line 1";
    document.getElementById("ModalNewAddressLine2id").value = "Line 2";
    document.getElementById("ModalNewAddressLine3id").value = "Line 3";
    document.getElementById("ModalNewAddressLine4id").value = "Line 4";
    document.getElementById("ModalNewAddressLine5id").value = "Line 5";
    document.getElementById("ModalNewAddressCodeid").value = "Address Code";
}


function DisplayModalRegisterNewItem() {
     //before displaying the modal ensure all the data input and error fields 
     //are blank.
    document.getElementById("ModalRegisterNewItemCodeid").value = "";
    $("span[data-valmsg-for='ModalRegisterNewItemCode'").text("");

    document.getElementById("ModalRegisterNewItemNameid").value = "";
    $("span[data-valmsg-for='ModalRegisterNewItemName'").text("");

    document.getElementById("ModalRegisterNewItemBrandid").value = "";
    $("span[data-valmsg-for='ModalRegisterNewItemBrand'").text("");

    document.getElementById("ModalRegisterNewItemDescriptionid").value = "";
    $("span[data-valmsg-for='ModalRegisterNewItemDescription'").text("");

    document.getElementById("ModalRegisterNewItemPriceid").value = "";
    $("span[data-valmsg-for='ModalRegisterNewItemPrice'").text("");

    document.getElementById("ModalRegisterNewItemTaxCodeid").value = "";
    $("span[data-valmsg-for='ModalRegisterNewItemTaxCode'").text("");
    // display modal
    $('#ModalRegisterNewItem').modal('show');
}
function PostItemtoDatabase() {
    var data = GetModalRegisterNewItemDataObject();
    var url = '/PurchaseOrder/SaveItem?Code=' + data.Code +
        '\&Description=' + data.Description +
        '\&Name=' + data.Name +
        '\&Brand=' + data.Brand +
        '\&Price=' + data.Price +
        '\&TaxCode=' + data.TaxCode +
        '\&Organisationid=' + document.getElementById("Toid").value;

    var xhr = new XMLHttpRequest();
    xhr.onreadystatechange = function () {
        if (this.readyState === 4 && this.status === 200) {
            if (xhr.responseText !== "Failure") {
                AddOption("OrgItemsid", data.Name, xhr.responseText);               
                AddItemToPO(data);
                $('#ModalRegisterNewItem').modal('hide');
            }
            return xhr.responseText;
        }
    };
    xhr.open('get', url);
    xhr.send();
}
function ModalRegisterNewItemSave() {
    if (ValidateForm("ModalRegisterNewItemForm") === "Passed") {

        var SavetoDb = PostItemtoDatabase();
    }
    else {
        alert("failed validation");
    }
}
function GetItemFromDatabase(itemid) {
    var xhr = new XMLHttpRequest();
    xhr.onreadystatechange = function () {
        if (this.readyState === 4 && this.status === 200) {
            console.log(xhr.responseText);
            var result = JSON.parse(xhr.responseText);
            AddItemToPO(result);
            // now reparse the page for elements to validate.
            $("form").removeData("validator").removeData("unobtrusiveValidation");
            //Parse the form again
            $.validator.unobtrusive.parse($("form"));
        }
    };
    var url = '/PurchaseOrder/GetItemFromDatabase?Itemid=' + itemid;
    xhr.open('get', url);
    xhr.send();
}

function CheckSelectset(elementid)
{    var V = document.getElementById(elementid).value;
    if (V === "Select" || V === "New") {
        return "Not set";
    }
    else
    {
        return "set";
    }   
}
//function ValidateForm(FormName) {
//    //  alert(document.getElementById('Toid').value);

//    var Valid = "True";
//    if (CheckSelectset('Toid') === "Not set")
//    {
//        document.getElementById('ToSpan').innerHTML = "Required value";
//        Valid = "False";
//    }
//    if (CheckSelectset('InvoiceToid') === "Not set") {
//        document.getElementById('InvoiceToSpan').innerHTML = "Required value";
//        Valid = "False";
//    }

//    if (CheckSelectset('DeliverToid') === "Not set") {
//        document.getElementById('DeliverToSpan').innerHTML = "Required value";
//        Valid = "False";
//    }

//    //document.getElementById('InvoiceToSpan').innerHTML = "test value";
//    //document.getElementById('DeliverToSpan').innerHTML = "test value";

//    $("form").removeData("validator").removeData("unobtrusiveValidation");
//    //Parse the form again
//    $.validator.unobtrusive.parse($("form"));
//    var Form = $("#" + FormName);
//    if (Form.valid() && Valid ==="True") {
//        return "Passed";
//    }
//    else {
//        return "Failed";
//    }
//}
function GetModalRegisterNewItemDataObject() {
    // define object
    var ToReturn =
    {
        Code: "",
        Name: "",
        Brand: "",
        Description: "",
        Price: "",
        TaxCode: ""
    };
    // fill object  
    ToReturn.Code = document.getElementById("ModalRegisterNewItemCodeid").value;
    ToReturn.Name = document.getElementById("ModalRegisterNewItemNameid").value;
    ToReturn.Brand = document.getElementById("ModalRegisterNewItemBrandid").value;
    ToReturn.Description = document.getElementById("ModalRegisterNewItemDescriptionid").value;
    ToReturn.Price = document.getElementById("ModalRegisterNewItemPriceid").value;
    ToReturn.TaxCode = document.getElementById("ModalRegisterNewItemTaxCodeid").value;
    // return populated object
    return ToReturn;
}
function TallyItems() {
    var Total = 0;
    var Price = 0;
    var Tax = 0;
    var TSet = document.getElementById('Totalid');
    TSet.value = Total;
    var POItems = document.querySelectorAll(".PurchaseOrderItem");
    POItems.forEach((ItemLine) => {
        if (ItemLine.id !== "ItemTemplate") {
            EPrefix = ItemLine.id;
            var E = ItemLine.getElementsByTagName('input');
            var ItemTax = 0;
            var ItemTotal = 0;
            var ItemPrice = 0;
            var Qty = EPrefix + 'QuantityId';
            var TC = EPrefix + 'TaxCodeId';
            var P = EPrefix + 'PriceId';
            var T = EPrefix + 'TaxId';
            var TotalP = EPrefix + 'TotalId';
            ItemTax = parseFloat(E[TC].value * (E[Qty].value * E[P].value));
            ItemTotal = parseFloat(ItemTax + (E[Qty].value * E[P].value));
            ItemPrice = parseFloat((E[Qty].value * E[P].value));
            Price = Price + ItemPrice;
            Tax = Tax + ItemTax;
            Total = Total + ItemTotal;
            E[T].value = ItemTax.toFixed(2);
            E[TotalP].value = ItemTotal.toFixed(2);
        }
    });
    document.getElementById('Totalid').value = Total.toFixed(2);
    document.getElementById('Taxid').value = Tax.toFixed(2);
    document.getElementById('Priceid').value = Price.toFixed(2);
}
function RemoveItem(id) {
    alert("Remove Item");
    $("#" + id).remove();
    TallyItems();
}
function AddItemToPO(result) {
    var Template = $('#ItemTemplate').clone();
    //  determine Item prefix used to set the element names and ids on the PO Item.
    //  Note prefix is used only here. 
    var Itemprefix = Math.random().toString(36).substr(2, 5);

    Template.attr('id', Itemprefix);  // set id
    Template.attr('style', 'visibility:visible').attr('class','PurchaseOrderItem'); // make visible
    Template.find(":button").attr("onclick", "POItemDelete('" + Itemprefix + "')");  

    Template.find("#TemplateDBLineId").attr("id", Itemprefix + "DBLineId").attr("name", Itemprefix + "DBLine").val(Itemprefix);

    Template.find("#TemplateCodeInput").attr("id", Itemprefix + "CodeId").attr("name", Itemprefix + "Code").val(result.Code);
    Template.find("#TemplateBrandInput").attr("id", Itemprefix + "BrandId").attr("name", Itemprefix + "Brand").val(result.Brand);
    Template.find("#TemplateNameInput").attr("id", Itemprefix + "NameId").attr("name", Itemprefix + "Name").val(result.Name);
    Template.find("#TemplateQuantityInput").attr("id", Itemprefix + "QuantityId").attr("name", Itemprefix + "Quantity").val(1);
    Template.find("#TemplateQuantitySpan").attr("id",Itemprefix+"Span").attr("data-valmsg-for", Itemprefix + "Quantity");

    Template.find("#TemplatePriceInput").attr("id", Itemprefix + "PriceId").attr("name", Itemprefix + "Price").val(result.Price);
    Template.find("#TemplatePriceSpan").attr("id", Itemprefix + "PriceSpan").attr("data-valmsg-for", Itemprefix + "Price");

    Template.find("#TemplateDescriptionInput").attr("id", Itemprefix + "DescriptionId").attr("name", Itemprefix + "Description").val(result.Description);
    Template.find("#TemplateTaxInput").attr("id", Itemprefix + "TaxId").attr("name", Itemprefix + "Tax").val(result.Tax);
    Template.find("#TemplateTaxCode").attr("id", Itemprefix + "TaxCodeId").val(result.TaxRate);

    Template.find("#TemplateTotalInput").attr("id", Itemprefix + "TotalId").attr("name", Itemprefix + "Total").val(result.Total);
    Template.find("#TemplateButtonRemoveItem").attr("id", Itemprefix + "RemoveButton").attr("onclick", "RemoveItem('" + Itemprefix + "')"); 
    
    $(Template).appendTo('#ItemsList'); // append
    TallyItems();
}
function GetPOFormData() {

    var ToReturn =
    {            
        //"RowVersionNo": document.getElementById("RowVersionNo").value,
        "Code": document.getElementById("Code").value,
        "DateRaised": document.getElementById("DateRaised").value,
        "Status": document.getElementById("Status").value,
        "BudgetCode": document.getElementById("BudgetCodesid").value,      
        "DateRequiredBy": document.getElementById("DateRequired").value,
        "Note": document.getElementById("Note").value,
        "To": document.getElementById("Toid").value,
        "ToEmail": document.getElementById("Toid").selectedOptions[0].getAttribute("data-email"),
        "ToDetail": document.getElementById("ToDetailId").innerHTML,
        "DeliverTo": document.getElementById("DeliverToid").value,
        "DeliverToDetail": document.getElementById("DeliverToDetailId").innerHTML,
        "InvoiceTo": document.getElementById("InvoiceToid").value,
        "InvoiceToDetail": document.getElementById("InvoiceToDetailId").innerHTML,
        "Price": document.getElementById("Priceid").value,
        "Tax": document.getElementById("Taxid").value,
        "Total": document.getElementById("Totalid").value, 
        "LineItem": []
    };
    var POItems = document.querySelectorAll(".PurchaseOrderItem");
    POItems.forEach((ItemLine) => {        
       ItemPrefix = ItemLine.id;
       var LineItems = ItemLine.getElementsByTagName('input');
       var DBLineId = ItemPrefix + "DBLineId";
       var taxcode = ItemPrefix + 'TaxCodeId';
       var brand = ItemPrefix + 'BrandId';
       var code = ItemPrefix + "CodeId";
       var name = ItemPrefix + "NameId";
       var description = ItemPrefix + "DescriptionId";
       var price = ItemPrefix + 'PriceId';
       var quantity = ItemPrefix + 'QuantityId';
       var tax = ItemPrefix + 'TaxId';
       var total = ItemPrefix + 'TotalId';
            ToReturn.LineItem.push(
                {
                    "DBLineId": LineItems[DBLineId].value,
                    //"id": LineItems[id].value,
                    //"RowVersionNo": LineItems[RVN].value,
                    "taxcode": LineItems[taxcode].value,
                    "brand":LineItems[brand].value,
                    "code": LineItems[code].value,
                    "name": LineItems[name].value,
                    "description": LineItems[description].value,
                    "price": LineItems[price].value,
                    "quantity": LineItems[quantity].value,
                    "tax": LineItems[tax].value,
                    "total": LineItems[total].value
                });
        
    });
    return ToReturn;
}

function PostPOtoDatabase(PO) {
    var url = '/PurchaseOrder/SavePO'; 
    var xhr = new XMLHttpRequest();
    xhr.open('post', url,true);
    xhr.setRequestHeader('Content-Type', 'application/json;charset=UTF-8');
    xhr.responseType = "text";
    xhr.onreadystatechange = function () {
        if (this.readyState === 4 && this.status === 200) {
        
            if (xhr.responseText !== "Failure") {
                alert("Purchase Order Saved ");
                //return "Purchase Order Saved";
            } else
            {
                alert("Something happend, Save failed.");
                //return "Something happend, Save failed.";
            }
        }
    };   
    xhr.send(JSON.stringify(PO));
}





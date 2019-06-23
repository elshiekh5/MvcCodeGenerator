var FormController = {
    form: null,
    objectPost: null,
    formDialog: null,
    TargetMethod: null,
    //-----------------------------------------------
    //Save
    //-----------------------------------------------
    Save: function () {
        if (this.TargetMethod == "Add")
        {
            this.Add();
           // this.TargetMethod = null;
        }
        else if (this.TargetMethod == "Edit")
        {
            this.Edit();
           // this.TargetMethod = null;
        }
        else
        {
            alert("Error:TargetMethod:" + this.TargetMethod);
        }
    },
    //-----------------------------------------------
    //Add
    //-----------------------------------------------
    Add: function () {
        if (this.form.formId.valid()) {
            this.form.LoadObject();
            $.ajax({
                type: 'POST',
                data: JSON.stringify(FormController.objectPost),
                url: FormController.form.AddURl,
                dataType: 'json',
                contentType: 'application/json',
                error: function (XMLHttpRequest, textStatus, errorThrown) { alert(errorThrown); },
                success: function (result) {
                    // insert new list into grid
                    if (FormController.form.autoRefresh == true) {
                        FormController.form.list.flexAddData(result);
                    }
                    FormController.CloseDialog();
                }
            });
        }
        else
            return false;
    },
    //-----------------------------------------------
    //-----------------------------------------------
    //Edit
    //-----------------------------------------------
    Edit: function () {
        if (this.form.formId.valid()) {
            this.form.LoadObject();
            $.ajax({
                type: 'POST',
                data: JSON.stringify(FormController.objectPost),
                url: FormController.form.UpdateURl,
                dataType: 'json',
                contentType: 'application/json',
                error: function (XMLHttpRequest, textStatus, errorThrown) { alert(errorThrown); },
                success: function (result) {
                    // insert new list into grid
                    if (FormController.form.autoRefresh == true) {
                        FormController.form.list.flexAddData(result);
                    }
                    FormController.CloseDialog();
                }
            });
        }
        else
            return false;
    },
    //-----------------------------------------------
    //-----------------------------------------------
    //Delete
    //-----------------------------------------------
    Delete: function (ids) {
        $.ajax({
            type: 'POST',
            data: JSON.stringify(ids),
            url: FormController.form.DeleteUrl,
            dataType: 'json',
            contentType: 'application/json',
            error: function (XMLHttpRequest, textStatus, errorThrown) { alert(errorThrown); },
            success: function (result) {
                // insert new list into grid
                if (FormController.form.autoRefresh == true) {
                    FormController.form.list.flexAddData(result);
                }
            }
        });
    },
    FireAdd: function (id) {
        this.TargetMethod = "Add";
        this.OpenDialog();
    },
    //-----------------------------------------------
    //Get object data then load his values
    //-----------------------------------------------
    LoadData: function (id) {
        $.ajax({
            type: "GET",
            url: encodeURI(FormController.form.GetObjeUrl + id),
            cache: false,
            dataType: 'json',
            error: function (XMLHttpRequest, textStatus, errorThrown) { alert(errorThrown); },
            success: function (data, textStatus, XMLHttpRequest) {

                FormController.TargetMethod = "Edit";
                FormController.form.LoadControls(data);
            },
            complete: function (XMLHttpRequest, textStatus) {

                FormController.OpenDialog();
            }
        });
    },
    //-----------------------------------------------
    //initialize object
    //-----------------------------------------------
    InitDialog: function (width, height) {
        this.formDialog.dialog({
            autoOpen: false,
            height: height,
            width: width,
            modal: true,
            buttons: {
                "Save": function () {
                    FormController.Save();

                },
                Cancel: function () {
                    FormController.TargetMethod = null;
                    FormController.CloseDialog();
                }
            },
            close: function () {
                FormController.TargetMethod = null;
                FormController.form.Clear();

            },
            open: function () {
                //$("#add-edit-dialog").parent().appendTo($("#NewsForm"));
            }
        });

    },
    OpenDialog: function () {
        this.formDialog.dialog("open");
    }
            ,
    CloseDialog: function () {
        this.TargetMethod = null;
        this.formDialog.dialog("close");
        this.form.Clear();
    },
    //-----------------------------------------------
    //initialize object
    //-----------------------------------------------
    Init: function (frm, obj, frmdlgId, width, height) {
        this.form = frm,
        this.objectPost = obj,
        this.formDialog = $(frmdlgId);
        //this.form.InitValidator();
        this.InitDialog(width, height);
        this.form.Clear();
    },
    FormatJsonDate: function (jsonDate) {
        var date = new Date(parseInt(jsonDate.substr(6)));
        var datestring = "";
        if (date != null) {
            datestring = ((date.getMonth() + 1) + "/" + date.getDate() + "/" + date.getFullYear());
        }
        return datestring;
    },
    
};
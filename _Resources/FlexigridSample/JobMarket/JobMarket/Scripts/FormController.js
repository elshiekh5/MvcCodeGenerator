var FormController = {
    form: null,
    formDialog: null,
    //-----------------------------------------------
    //Save and Edit
    //-----------------------------------------------
    Save: function () {
        if ($(this.form.formId).valid()) {
            alert($(this.form.formId).valid());
            this.form.LoadObject();
            $.ajax({
                type: 'POST',
                data: JSON.stringify(jobPost),
                url: FormController.form.SaveURl,
                dataType: 'json',
                contentType: 'application/json',
                success: function (result) {
                    FormController.form.Clear();
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
            success: function (result) {
                // insert new list into grid
                if (FormController.form.autoRefresh == true) {
                    FormController.form.list.flexAddData(result);
                }
            }
        });
    },
    //-----------------------------------------------
    //Get object data then load his values
    //-----------------------------------------------
    LoadData: function (id) {
        $.ajax({
            type: "GET",
            url: encodeURI(FormController.form.GetObjectUrl + id),
            cache: false,
            dataType: 'json',
            error: function (XMLHttpRequest, textStatus, errorThrown) { alert(errorThrown); },
            success: function (data, textStatus, XMLHttpRequest) { FormController.form.LoadControls(data); },
            complete: function (XMLHttpRequest, textStatus) { FormController.OpenDialog(); }
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
                    FormController.CloseDialog();
                }
            },
            close: function () {
                FormController.form.Clear();

            },
            open: function () {
                //$("#add-edit-dialog").parent().appendTo($("#add-edit-form"));
            }
        });

    },
    OpenDialog: function () {
        this.formDialog.dialog("open");
    }
            ,
    CloseDialog: function () {
        this.formDialog.dialog("close");
    },
    //-----------------------------------------------
    //initialize object
    //-----------------------------------------------
    Init: function (frm, frmdlgId) {
        this.form = frm,
        this.formDialog = $(frmdlgId);
        this.form.InitValidator();
        this.InitDialog(380, 500);
    }
};
var FormController = {
    form: null,
    formDialog: null,
    //-----------------------------------------------
    //Save and Edit
    //-----------------------------------------------
    Save: function () {
        if ($("#NewsForm").valid()) {
            this.form.LoadObject();
            $.ajax({
                type: 'POST',
                data: JSON.stringify(JobPost),
                url: FormController.form.SaveURl,
                dataType: 'json',
                contentType: 'application/json',
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
            url: encodeURI(FormController.form.GetObjeUrl + id),
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
                //$("#add-edit-dialog").parent().appendTo($("#NewsForm"));
            }
        });

    },
    OpenDialog: function () {
        this.formDialog.dialog("open");
    }
            ,
    CloseDialog: function () {
        
        this.formDialog.dialog("close");
        this.form.Clear();
    },
    //-----------------------------------------------
    //initialize object
    //-----------------------------------------------
    Init: function (frm, frmdlgId,width,height) {
        this.form = frm,
        this.formDialog = $(frmdlgId);
        //this.form.InitValidator();
        this.InitDialog(width, height);
    }
};
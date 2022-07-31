jQuery(document).ready(function ($) {
      
    $(document.body).on('run_ajax_filter', function ajaxFilter(event, args) {


        var initData = args.data;
        var filterResult;

        $.ajax({

            type: "POST",
            url: args.ajax_url,
            dataType: "text",
            data: initData,
            success: function (res) {

                filterResult = $(res).find(args.find);
                $(args.find).replaceWith(filterResult);
            },
            error: function (req, status, error) {
                console.log(error);
            }
        });
    });

})
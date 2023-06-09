$(document).ready(function()
{

    // activate Nestable for list 1
    jQuery.noConflict();
    $('#nestable1').nestable({
        group: 1
    });
    
    // activate Nestable for list 2
    jQuery.noConflict();
    $('#nestable2').nestable({
        group: 1
    });
    var $expand = false;
    $('#nestable-menu').on('click', function(e)
    {
        if ($expand) {
            $expand = false;
            $('.dd').nestable('expandAll');
        }else {
            $expand = true;
            $('.dd').nestable('collapseAll');
        }
    });

    $('#nestable3').nestable();

});
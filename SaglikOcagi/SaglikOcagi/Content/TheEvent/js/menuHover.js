
$(document).ready(function () {
   $('#menuHover #deneme li').click(function () {

        $("li.menu-active").removeClass("menu-active");
        $(this).addClass('menu-active');     
        return false;   
    }); 
});
   
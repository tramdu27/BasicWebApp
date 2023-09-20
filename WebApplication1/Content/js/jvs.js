<script>
        var password1Element = document.getElementById("password1");
        var password2Element = document.getElementById("password2");

        /*password1Element.onblur = checkPasswords;*/
        password2Element.onblur = checkPasswords;

        function checkPasswords() {}
        var password1 = password1Element.value;
        var password2 = password2Element.value;

        if (password1 === password2) {alert("Mật khẩu trùng khớp.")};
         else {alert("Mật khẩu không trùng khớp.")};
        
</script>

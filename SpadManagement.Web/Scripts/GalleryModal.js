// Get the modal

function openModal(imageId){

    debugger;
    var div = document.getElementById('modalBtn');
    div.innerHTML  = '';

    var modal = document.getElementById("imageModal");
    
    // Get the image and insert it inside the modal - use its "alt" text as a caption
    var img = document.getElementById(imageId);
    var modalImg = document.getElementById("popUpImage");
    var captionText = document.getElementById("caption");

    //img.onclick = function(){
      modal.style.display = "block";
      modalImg.src = img.src;
      //captionText.innerHTML = this.alt;
    //}    
    
       var profileBtn='<div class="btn btn-primary" style="margin:3px;" onClick="setAsProfileImage('+'\'' +imageId+ '\''+')">ثبت به عنوان تصویر پروفایل</div>';
       var deleteBtn='<div class="btn btn-danger" style="margin:3px;" onClick="deleteImage('+'\'' +imageId+ '\''+' )">حذف تصویر</div>';
       var approveBtn=  '<div class="btn btn-success" style="margin:3px;" onClick="AcceptImage('+'\'' +imageId+ '\''+' )">تایید تصویر</div>';

        //var btns =profileBtn + approveBtn + deleteBtn;
        var btns =profileBtn + deleteBtn;

        $('div[class="modalBtn"').append(btns);                
}

function onClose(){
    var modal = document.getElementById("imageModal");
  modal.style.display = "none";
}

function setAsProfileImage(imageId){
    var url =  '/Customer/SetImageAsProfile/'+ imageId;

    $.ajax({
        type: "POST",
        data: {},
        traditional: true,
        contentType: "application/json; charset=utf-8",
        url:url ,
        dataType: "json",
        async: true,
        success: function (response) {
            if(response){
                alert('عملیات با موفقیت انجام شد');
                onClose();
                location.reload();
            }  
            else{
                alert('خطایی رخ داد');
            }
        }
    });
}

function deleteImage(imageId){
    var url =  '/Customer/DeleteImage/'+ imageId;

    if(confirm('آیا اطمینان دارید؟')){
        $.ajax({
            type: "POST",
            data: {},
            traditional: true,
            contentType: "application/json; charset=utf-8",
            url:url ,
            dataType: "json",
            async: true,
            success: function (response) {
                if(response){
                   location.reload();
                   alert('عملیات با موفقیت انجام شد');
                }  
                else{
                    alert('خطایی رخ داد');
                }
            }
        });
    }
}



// Get the <span> element that closes the modal
//var span = document.getElementsById("close")[0];

// When the user clicks on <span> (x), close the modal
//span.onclick = function() {
//  modal.style.display = "none";
//}
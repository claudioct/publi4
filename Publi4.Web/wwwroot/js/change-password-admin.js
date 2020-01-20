$(function () {
  $("#resetPasswordModal").modal({
    keyboard: true,
    backdrop: "static",
    show: false

  }).on("show",
    function () {

    });

  $(".tooltip-wrapper").find("a[data-id]").on("click",
    function () {
      //do all your operation populate the modal and open the modal now. DOnt need to use show event of modal again
      $("#userId").html($(this).data("id"));
      $("#userEmail").html($(this).data("email"));
      $("#sendReset").attr("data-id", $(this).data("id"));
      $("#sendReset").attr("data-email", $(this).data("email"));
      $("#resetPasswordModal").modal("show");
    });

});

function SendReset(identifier) {
  var userId = $(identifier).attr("data-id");
  $.post("/Account/ForgotPasswordAdmin",
    { id: userId },
    function (data) {
      console.log(data);
      if (data.result === "Ok") {
          toastr.success("Requisição de troca de senha enviada.", "Troca de senha");
      } else if (data.result === "Bad Request") {
        toastr.error("Requisição de troca de senha não pode ser enviada. <br/> Tente novamente mais tarde.", "Troca de senha");
      } else {
          toastr.warning("Email do usuário não foi confirmado. <br/> Foi enviadado um novo email de confirmação.", "Troca de senha");
      }
    });
}
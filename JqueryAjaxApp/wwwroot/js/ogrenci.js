$(document).ready(function () {
    // Sayfa yüklendiğinde öğrenci listesini getir
    loadOgrenciListe();

    // Form submit olayını engelle ve Ajax ile gönder
    $("#ogrenciEkleForm").on("submit", function (e) {
        e.preventDefault();
        ekleOgrenci();
    });

    // Güncellenecek öğrencinin bilgilerini form içine yükle
    $(document).on("click", ".btn-duzenle", function () {
        const id = $(this).data("id");
        getOgrenciById(id);
    });

    // Güncelleme formunun submit olayını engelle ve Ajax ile gönder
    $("#ogrenciGuncelleForm").on("submit", function (e) {
        e.preventDefault();
        guncelleOgrenci();
    });

    // Öğrenci silme işlemi
    $(document).on("click", ".btn-sil", function () {
        const id = $(this).data("id");
        if (confirm("Bu öğrenciyi silmek istediğinize emin misiniz?")) {
            silOgrenci(id);
        }
    });
});

// Tüm öğrencileri getir ve tabloya doldur
function loadOgrenciListe() {
    $.ajax({
        url: "/Ogrenci/OgrenciListeAjax",
        type: "GET",
        success: function (data) {
            let tableRows = "";
            $.each(data, function (index, ogrenci) {
                tableRows += `
                    <tr>
                        <td>${ogrenci.ogrenciid}</td>
                        <td>${ogrenci.ad}</td>
                        <td>${ogrenci.soyad}</td>
                        <td>
                            <button class="btn btn-sm btn-primary btn-duzenle" data-id="${ogrenci.ogrenciid}">Düzenle</button>
                            <button class="btn btn-sm btn-danger btn-sil" data-id="${ogrenci.ogrenciid}">Sil</button>
                        </td>
                    </tr>`;
            });
            $("#ogrenciListesiTbody").html(tableRows);
        },
        error: function (error) {
            console.log("Hata oluştu:", error);
            alert("Öğrenci listesi yüklenirken bir hata oluştu.");
        }
    });
}

// ID'ye göre öğrenci bilgilerini getir
function getOgrenciById(id) {
    $.ajax({
        url: `/Ogrenci/OgrenciGetirAjax?id=${id}`,
        type: "GET",
        success: function (data) {
            $("#guncelleId").val(data.ogrenciid);
            $("#guncelleAd").val(data.ad);
            $("#guncelleSoyad").val(data.soyad);
            $("#guncelleModal").modal('show');
        },
        error: function (error) {
            console.log("Hata oluştu:", error);
            alert("Öğrenci bilgileri getirilirken bir hata oluştu.");
        }
    });
}

// Yeni öğrenci ekle
function ekleOgrenci() {
    const ogr = {
        Ad: $("#ekleAd").val(),
        Soyad: $("#ekleSoyad").val()
    };

    $.ajax({
        url: "/Ogrenci/OgrenciEkleAjax",
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify(ogr),
        success: function (response) {
            if (response.success) {
                alert(response.message);
                $("#ekleModal").modal('hide');
                $("#ogrenciEkleForm")[0].reset();
                loadOgrenciListe();
            } else {
                alert(response.message);
            }
        },
        error: function (xhr, status, error) {
            console.log("Hata Detayları:", xhr.responseText, status, error);
            alert("Öğrenci eklenirken bir hata oluştu: " + (xhr.responseJSON?.message || error));
        }
    });
}

// Öğrenci güncelle
function guncelleOgrenci() {
    const ogrenci = {
        OgrenciId: $("#guncelleId").val(),
        Ad: $("#guncelleAd").val(),
        Soyad: $("#guncelleSoyad").val()
    };

    $.ajax({
        url: "/Ogrenci/OgrenciGuncelleAjax",
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify(ogrenci),
        success: function (response) {
            if (response.success) {
                alert(response.message);
                $("#guncelleModal").modal('hide');
                loadOgrenciListe();
            } else {
                alert(response.message);
            }
        },
        error: function (xhr, status, error) {
            console.log("Hata Detayları:", xhr.responseText, status, error);
            alert("Öğrenci güncellenirken bir hata oluştu: " + (xhr.responseJSON?.message || error));
        }
    });
}

// Öğrenci sil
function silOgrenci(id) {
    $.ajax({
        url: "/Ogrenci/OgrenciSilAjax",
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify({ id: id }),
        success: function (response) {
            if (response.success) {
                alert(response.message);
                loadOgrenciListe();
            } else {
                alert(response.message);
            }
        },
        error: function (xhr, status, error) {
            console.log("Hata Detayları:", xhr.responseText, status, error);
            alert("Öğrenci silinirken bir hata oluştu: " + (xhr.responseJSON?.message || error));
        }
    });
}
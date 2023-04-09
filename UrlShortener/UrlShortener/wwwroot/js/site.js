// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
class ApiClientHelper {
    constructor() {
        /**
         * HTTP Types Helper
         */
        this.httpTypes = {
            GET: 'GET',
            POST: 'POST',
            PUT: 'PUT',
            DELETE: 'DELETE'
        };
    }
    /**
     * 
     * @param {*} type to decide the ajax call http type 
     * @param {*} url request url
     * @param {*} datatype the type of data that will be sent in the request
     * @param {*} data request data
     * @param {*} loaderBeforeStart show laoder before start (will need to close the loader in the (then \ catch method)) 
     * @returns 
     */
    makeAjaxCall(type, url, datatype, data, contentType = null, loaderBeforeStart = false) {
        console.log("makeAjaxCall", `type :${type} ,url :${url} ,datatype :${datatype} ,data :${data} , contentType :${contentType}`);
        return new Promise((resolve, reject) => {
            try {
                if (loaderBeforeStart) {
                    //show loader
                    //loadingWheelPop("open");
                }

                $.ajax({
                    type: type,
                    url: url,
                    datatype: datatype,
                    data: data,
                    contentType: contentType,
                    success: function (response) {
                        console.log("makeAjaxCall", `response :${response}`);
                        resolve({ status: "success", response: response });
                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                        console.log("makeAjaxCall", `error :${JSON.stringify(jqXHR)}`);
                        console.log(jqXHR)
                        reject({ status: "error", response: JSON.stringify(jqXHR) });
                    }
                });
            } catch (error) {
                console.log("makeAjaxCall", `error :${JSON.stringify(error)}`);
                console.log(error)
                reject({ status: "error", response: error });
            }
        });
    }
}

getUserStatistic = () => {
    let urlPath = `${window.location.origin}/getUserStatistic`;
    return new ApiClientHelper().makeAjaxCall(new ApiClientHelper().httpTypes.GET, urlPath, "json", null, "application/json")
};


$(function () {
    generateUrl = (LongUrl, selectedDate) => {
        let urlPath = `${window.location.origin}/shortenUrl`;
        return new ApiClientHelper().makeAjaxCall(new ApiClientHelper().httpTypes.POST, urlPath, "json", JSON.stringify({
            "url": LongUrl,
            "expiryDate": selectedDate
        }), "application/json")
    };

    function isValidUrl(str) {
        let givenURL;
        try {
            givenURL = new URL(str);
        } catch (error) {
            console.log("error is", error)
            return false;
        }
        return givenURL.protocol === "http:" || givenURL.protocol === "https:";
    }

    var linkTemplate = (shortlink) => {
        var formatedUrl = `${window.location.origin}/r/${shortlink}`;
        return `<div class="card">
                <div class="card-body">
                    <h5 class="card-title">${shortlink}</h5>
                    <p class="card-text">
                        <span id="short-url"></span>
                        <a href="${formatedUrl}" id="launch-btn" class="btn btn-primary btn-sm ml-2">
                            <i class="fa fa-external-link-alt"></i>
                        </a>
                    </p>
                </div>
            </div>`;
    }

    $('#shorten-form').submit(function (event) {
        event.preventDefault();
        var error = 0;
        $('#url-error-msg').addClass('d-none');
        $('#loader').removeClass('d-none');

        var url = $('#url-input').val();
        var expiryDate = $('#expiry-date-input').val();

        var selectedDate = moment(expiryDate);
        selectedDate.format("YYYY-MM-DD");

        console.log(expiryDate)
        console.log(selectedDate)
        //var now = new Date();
        //var selectedDate = new Date(expiryDate);
        //if (selectedDate < now) {
        //    $('#expiry-date-error-msg').removeClass('d-none');
        //    error++;
        //}

        if (!url || !isValidUrl(url)) {
            $('#url-error-msg').removeClass('d-none')
            $('#loader').addClass('d-none');
            error++;
        }
        if (error === 0) {
            generateUrl(url, selectedDate._i).then((result) => {
                console.log(result)
                var newUrl = result.response.hash;

                $("#links #links-container").append(linkTemplate(newUrl))
                $('#loader').addClass('d-none');
            }).catch((error) => { $('#url-error-msg').addClass('d-none'); $('#loader').addClass('d-none'); });
        }
    });
});
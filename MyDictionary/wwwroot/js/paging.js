
const paging = {

    state: {
        querySet: '',
        page: '',
        rows: '',
        window: '',
    },

    setPages: (page, rows, window) => {
        paging.state.page = page;
        paging.state.rows = rows;
        paging.state.window = window;
    },

    loadData: async (url, buildFunc) => {
        paging.state.querySet = await fetch(url)
            .then(response => { return response.json() });

        paging.buildTable = buildFunc;
        paging.buildTable();
    },

    pagination: (querySet, page, rows) => {

        var trimStart = (page - 1) * rows
        var trimEnd = trimStart + rows

        var trimmedData = querySet.slice(trimStart, trimEnd)

        var pages = Math.round(querySet.length / rows);

        return {
            querySet: trimmedData,
            pages: pages,
        }
    },

    pageButtons: (pages) => {
        var wrapper = document.getElementById('pagination-wrapper')

        wrapper.innerHTML = ``
        console.log('Pages:', pages)

        var maxLeft = (paging.state.page - Math.floor(paging.state.window / 2))
        var maxRight = (paging.state.page + Math.floor(paging.state.window / 2))

        if (maxLeft < 1) {
            maxLeft = 1
            maxRight = paging.state.window
        }

        if (maxRight > pages) {
            maxLeft = pages - (paging.state.window - 1)

            if (maxLeft < 1) {
                maxLeft = 1
            }
            maxRight = pages
        }

        for (var page = maxLeft; page <= maxRight; page++) {
            wrapper.innerHTML += `<button value=${page} class="page btn btn-sm btn-info">${page}</button>`
        }

        if (paging.state.page != 1) {
            wrapper.innerHTML = `<button value=${1} class="page btn btn-sm btn-info">&#171; First</button>` + wrapper.innerHTML
        }

        if (paging.state.page != pages) {
            wrapper.innerHTML += `<button value=${pages} class="page btn btn-sm btn-info">Last &#187;</button>`
        }

        $('.page').on('click', function () {
            $('#table-body').empty()

            paging.state.page = Number($(this).val())

            paging.buildTable()
        })

    },


    setRowTemplate: (buildFunction) => {
        buildTable = buildFunction;
    },

    //buildTable: () => { },

    //buildTable: () => {
        //var table = $('#table-body')

        //var data = paging.pagination(paging.state.querySet, paging.state.page, paging.state.rows)
        //var myList = data.querySet

        //for (var i = 1 in myList) {
        //    //Keep in mind we are using "Template Litterals to create rows"

        //    var row =

        //        //<a data-poload="/Home/GetWordJson/abhorrent" data-toggle="popover" href="/Home/GetWord?word=abhorrent" data-original-title="" title="" aria-describedby="popover697434">abhorrent</a>
        //        //<a data-poload="/Home/GetWordJson/abhorrent" data-toggle="popover" href="/Home/GetWord?word=abhorrent" data-original-title="" title="" aria-describedby="popover697434">abhorrent</a>
        //        `<tr>
        //            <td style="font-size: 16px; word-break: break-all">
        //                <a data-poload="/Home/GetWordJson/${myList[i].spelling}" data-toggle="popover" 
        //                    href="/Home/GetWord?word=${myList[i].spelling}" data-original-title="" 
        //                    title="" aria-describedby="popover697434">${myList[i].spelling}</a>
        //            </td>
        //            <td>
        //                <span style="">${new Date(myList[i].createdDate).toDateString().substring(4)}</span>
        //            </td>
        //            <td style="text-align: right">
        //                <button id="${myList[i].spelling} type="button" value="Delete" class="btn delete-word" data-target="#confirm-delete" data-toggle="modal">
        //                    <i class="fa fa-trash"></i>
        //                </button>
        //            </td>
        //        </tr>`
        //    table.append(row)
        //}

        //paging.pageButtons(data.pages)
    //}
}





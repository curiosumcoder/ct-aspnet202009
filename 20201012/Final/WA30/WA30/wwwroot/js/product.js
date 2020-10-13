document.addEventListener('DOMContentLoaded', () => {
    console.log('Document ready!');

    const filter = document.getElementById('filter');
    const bSearch = document.getElementById('bSearch');
    bSearch.addEventListener('click', (event) => {
        event.preventDefault();

        let ps = new ProductService();

        ps.getProducts().then((data) => {
            //console.log(data);

            let filtered = _.filter(data, function (p) {
                return p.productName.toLowerCase().
                    indexOf(filter.value.toLowerCase()) !== -1;
            });

            //console.log(filtered);

            var grouped = _.groupBy(filtered, "categoryID");
            console.log(grouped);

            for (var key in grouped) {
                var items = grouped[key];

                console.log(grouped[key][0].category.categoryName);
                appendCategory(grouped[key][0].category.categoryName);

                items.forEach(function (item, index, array) {
                    console.log(item);

                    appendProduct(item);
                });
            };

        }).catch((err) => {
            console.log(`Error: ${err}`);
        });
    });
});

function appendProduct(item) {
    document.getElementById('list').innerHTML += `<td>${item.productName}</td>
            <td>${item.quantityPerUnit}</td>
            <td>${item.unitPrice}</td>
            <td>${item.unitsInStock}</td>
            <td>${item.category.categoryName}</td>
            <td>${item.supplier.companyName}</td>`;
}

function appendCategory(categoryName) {
    document.getElementById('list').innerHTML += `<tr><td colspan='6'><h4>${categoryName}</h4></td></tr>`;
}

class ProductService {
    getProducts() {
        return fetch('js/index.json').then(
            (response) => {
                if (response.status !== 200) {
                    console.log('http not 200');
                }

                return response.json();
            });
    }
}
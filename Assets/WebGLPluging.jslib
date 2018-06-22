var loadFromUrlPlugin = {

    loadDataFromUrl = function(link){

        var url = Pointer_stringifyl(link);
        fetch(url).then(function(response){

            response.text().then(function(text){
                return text;

            })    
        });
    }
}

mergeInto(LibraryManager.library, loadFromUrlPlugin);
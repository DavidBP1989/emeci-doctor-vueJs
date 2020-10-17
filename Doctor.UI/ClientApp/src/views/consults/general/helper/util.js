function laboratory_cabinet_format(response) {
    let result = [];
    response.forEach(x => {
        let studies = [];
        x.StudiesList.forEach(n => {
            studies.push({
                id: n.Id,
                name: n.Name,
                check: false,
                edit: false
            })
        });

        result.push({
            id: x.Id,
            name: x.Name,
            style: false,
            studies: studies
        });
    })
    return result;
};

function diagnostic_treatments_format(response) {
    let result = [];
    response.forEach(x => {
        result.push({
            id: x.Id,
            name: x.GroupName,
            disabled: false,
            list: x.List
        })
    });
    return result;
};

function addLab_Cab_toReq(array) {
    let result = [];

    let group = array.filter(x => {
        return x.style;
    });
    group.forEach(x => {
        result.push({
            id: x.id,
            name: x.name,
            studies: x.studies.filter(s => {
                return s.check;
            })
        })
    });
    return result;
};

export { laboratory_cabinet_format, diagnostic_treatments_format, addLab_Cab_toReq }
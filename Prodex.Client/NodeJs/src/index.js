import BpmnModeler from 'bpmn-js/lib/Modeler';

window.loadBpmn = () => {

    var canv = document.querySelector('#bpmn-canvas');

    if (canv.hasChildNodes())
        return;

    window.modeler = new BpmnModeler({
        container: '#bpmn-canvas'
    });


    try {
        window.modeler.createDiagram();

        console.log('rendered');
    } catch (err) {
        console.log('error rendering', err);
    }
}

window.getCurrentDiagram = async () => {
    if (!window.modeler) return null;

    return (await window.modeler.saveXML({ format: true })).xml;
}



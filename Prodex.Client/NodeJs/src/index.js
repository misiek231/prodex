import BpmnJS from 'bpmn-js/lib/Modeler';
import './viewer.js';

window.loadBpmn = (currentInstance) => {

    var canv = document.querySelector('#bpmn-canvas');

    if (canv.hasChildNodes())
        return;

    window.modeler = new BpmnJS({
        container: '#bpmn-canvas'
    });

    window.modeler.on('element.click', async (e) => {
        await currentInstance.invokeMethodAsync('OnElementClick', e.element, e.element.businessObject.sourceRef);
    });


    try {
        window.modeler.createDiagram();
    } catch (err) {
        console.log('error rendering', err);
    }
}

window.getCurrentDiagram = async () => {
    if (!window.modeler) return null;
    var xml = (await window.modeler.saveXML({ format: true })).xml
    return xml;
}

window.setCurrentDiagram = async (xml) => {
    if (!window.modeler) return;

    await window.modeler.importXML(xml);
}




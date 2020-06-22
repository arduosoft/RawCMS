const _ConfigurationView = async (res, rej) => {
    const tpl = await RawCMS.loadComponentTpl(
        "/app/modules/core/views/configuration-view/configuration-view.tpl.html"
    );

    res({
        template: tpl
    });
};

export const ConfigurationView = _ConfigurationView;
export default _ConfigurationView;
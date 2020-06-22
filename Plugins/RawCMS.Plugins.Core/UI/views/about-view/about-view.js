const _AboutView = async (res, rej) => {
    const tpl = await RawCMS.loadComponentTpl(
        "/app/modules/core/views/about-view/about-view.tpl.html"
    );

    res({
        template: tpl
    });
};

export const AboutView = _AboutView;
export default _AboutView;
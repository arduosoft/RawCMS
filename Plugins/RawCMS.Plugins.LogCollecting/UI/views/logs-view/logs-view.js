const _LogsView = async (res, rej) => {
    const tpl = await RawCMS.loadComponentTpl(
        "/app/modules/logs/views/logs-view/logs-view.tpl.html"
    );

    res({
        template: tpl
    });
};

export const LogsView = _LogsView;
export default _LogsView;
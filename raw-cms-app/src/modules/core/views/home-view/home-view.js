import { DashboardDef } from '../../components/dashboard/dashboard.js';

const _HomeView = async (res, rej) => {
  const tpl = await RawCMS.loadComponentTpl('/modules/core/views/home-view/home-view.tpl.html');
  const dashboardDef = await DashboardDef();

  res({
    components: {
      Dashboard: dashboardDef,
    },
    template: tpl,
  });
};

export const HomeView = _HomeView;
export default _HomeView;

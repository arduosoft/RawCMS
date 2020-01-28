import { BaseCrudService } from '../../shared/services/base-crud-service.js';

class FullTextService extends BaseCrudService {
  constructor() {
    super({
      basePath: '/api/FullText/doc',
    });
  }

  async search({ page = 1, size = 20, query = undefined, sort = undefined } = {}, LogNameHash) {
    const config = { pageSize: size, pageNumber: page };
    if (query) {
      config.query = query;
    }
    if (sort) {
      config.sort = JSON.stringify(sort);
    }

    const res = await this._apiClient.get(`${this._basePath}/search/${LogNameHash}`, {
      params: config,
    });
    return res.data.data;
  }
  async getTextByLevel(logNameHash, level) {
    debugger;
    const res = await this.search(
      {
        size: 1,
        query: {
          level: level,
        },
      },
      logNameHash
    );
    return res;
  }
}

export const fullTextService = new FullTextService();
export default fullTextService;

import { BaseCrudService } from "/app/common/shared/services/base-crud-service.js";

class FullTextService extends BaseCrudService {
    constructor() {
        super({
            basePath: "/api/FullText/doc"
        });
    }

    async search(
        { page = 1, size = 20, searchQuery = undefined, sort = undefined } = {},
        LogNameHash
    ) {
        const config = { pageSize: size, pageNumber: page };
        if (searchQuery) {
            config.searchQuery = searchQuery;
        }
        if (sort) {
            config.sort = JSON.stringify(sort);
        }

        const res = await this._apiClient.get(
            `${this._basePath}/search/${LogNameHash}`,
            {
                params: config
            }
        );
        return res.data.data;
    }

}

export const fullTextService = new FullTextService();
export default fullTextService;

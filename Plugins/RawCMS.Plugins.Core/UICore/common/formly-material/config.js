import { BoolField } from "/app/common/formly-material/components/bool-field/bool-field.js";
import { DateField } from "/app/common/formly-material/components/date-field/date-field.js";
import { EntitiesListField } from "/app/common/formly-material/components/entities-list-field/entities-list-field.js";
import { FieldsListField } from "/app/common/formly-material/components/fields-list-field/fields-list-field.js";
import { IntField } from "/app/common/formly-material/components/int-field/int-field.js";
import { ListField } from "/app/common/formly-material/components/list-field/list-field.js";
import { NumberField } from "/app/common/formly-material/components/number-field/number-field.js";
import { RelationField } from "/app/common/formly-material/components/relation-field/relation-field.js";
import { TextField } from "/app/common/formly-material/components/text-field/text-field.js";

const _configFormlyMaterialModule = {
    name: "formly-material",

    init() {
        // Bool
        Vue.$formly.addType("bool", BoolField);
        // Strings
        Vue.$formly.addType("regexp", TextField);
        Vue.$formly.addType("text", TextField);
        // Numbers
        Vue.$formly.addType("number", NumberField);
        Vue.$formly.addType("int", IntField);
        // Date/Time
        Vue.$formly.addType("date", DateField);
        // List
        Vue.$formly.addType("list", ListField);
        // Relations
        Vue.$formly.addType("relation", RelationField);
        Vue.$formly.addType("entities-list", EntitiesListField);
        Vue.$formly.addType("fields-list", FieldsListField);
    }
};

export const configFormlyMaterialModule = _configFormlyMaterialModule;
export default _configFormlyMaterialModule;
import { RawCMS } from '../../../../config/raw-cms.js';
import { loginService } from '../../services/login.service.js';

const _LoginView = async (res, rej) => {
  const tpl = await RawCMS.loadComponentTpl('/modules/core/views/login-view/login-view.tpl.html');

  res({
    computed: {
      usernameErrors() {
        const errors = [];
        if (!this.$v.username.$dirty) {
          return errors;
        }
        !this.$v.username.required && errors.push(this.$t('core.common.requiredField'));
        return errors;
      },
      passwordErrors() {
        const errors = [];
        if (!this.$v.password.$dirty) {
          return errors;
        }
        !this.$v.password.required && errors.push(this.$t('core.common.requiredField'));
        return errors;
      },
    },
    data: function() {
      return {
        username: '',
        password: '',
        error: false,
      };
    },
    methods: {
      login: async function(submitEvent) {
        submitEvent.preventDefault();

        this.$v.$touch();
        if (this.$v.$invalid) {
          return;
        }

        try {
          await loginService.login(this.username, this.password);
        } catch {
          this.error = true;
          return;
        }

        if (this.$route.params.nextUrl != null) {
          this.$router.push(this.$route.params.nextUrl);
          return;
        }

        this.$router.push({ name: 'home' });
      },
      onInputEvt: function(input) {
        this.$v[input].$touch();
        this.error = false;
      },
    },
    mixins: [RawCMS.plugins.vuelidate.validationMixin],
    template: tpl,
    validations: {
      username: { required: RawCMS.utils.vuelidateValidators.required },
      password: {
        required: RawCMS.utils.vuelidateValidators.required,
      },
    },
  });
};

export const LoginView = _LoginView;
export default _LoginView;

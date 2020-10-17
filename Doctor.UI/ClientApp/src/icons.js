import Vue from 'vue';
import { library } from '@fortawesome/fontawesome-svg-core';
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome';
import { 
    faUserMd,
    faCaretDown,
    faSignOutAlt,
    faUnlockAlt,
    faSearch,
    faPhone,
    faKey,
    faAt,
    faCalendarAlt,
    faVenusMars,
    faCaretUp,
    faUser,
    faEdit,
    faTrashAlt,
    faTimesCircle,
    faCaretRight,
    faChevronLeft,
    faAlignJustify,
    faTimes,
    faPrint,
    faFolderOpen,
    faArchive,
    faSyringe,
    faCheck,
    faAddressBook
} from '@fortawesome/free-solid-svg-icons';
import { 
    faFacebookF,
    faTwitter
} from '@fortawesome/free-brands-svg-icons';

library.add(
    faUserMd,
    faCaretDown,
    faSignOutAlt, 
    faUnlockAlt,
    faSearch,
    faPhone,
    faFacebookF,
    faKey,
    faAt,
    faCalendarAlt,
    faVenusMars,
    faCaretUp,
    faUser,
    faEdit,
    faTrashAlt,
    faTimesCircle,
    faCaretRight,
    faChevronLeft,
    faAlignJustify,
    faTimes,
    faTwitter,
    faPrint,
    faFolderOpen,
    faArchive,
    faSyringe,
    faCheck,
    faAddressBook
);

Vue.component('fa-icon', FontAwesomeIcon);
module.exports = {
  content: ["./src/**/*.{js,jsx,ts,tsx}"],
  theme: {
    screens: {
      'sm': '440px',

      'md': '547px',

      'lg': '768px',

      'xl': '1024px',

      '2xl': '1680px',
    },
    extend: {
      colors:{
        primary: '#5999d2',
        secondary: '#190e34',
        background: '#dcdcdc',
        white: '#ffffff',
        grey: '#1A252F',
        orange: '#eb4034',
        purple:'#190e34'
      }
    },
  },
  plugins: [],
}

import React from 'react'
import { ThemeContext, themes } from '../contexts/ThemeContext'
import { FluentProvider, webDarkTheme, webLightTheme } from '@fluentui/react-components'

const getTheme = () => {
  const theme = `${window?.localStorage?.getItem('theme')}`
  switch (theme) {
    case themes.dark:
      document.documentElement.dataset.theme = themes.dark
      return webDarkTheme

    case themes.light:
      document.documentElement.dataset.theme = themes.light
      return webLightTheme

    case themes.none:
      return getSystemTheme();
    default:
      saveTheme(themes.none)
      return getSystemTheme();
  }
}
const getSystemTheme = () => {
  const userMedia = window.matchMedia('(prefers-color-scheme: light)')
  if (userMedia.matches) {
    document.documentElement.dataset.theme = themes.light
    return webLightTheme
  }
  document.documentElement.dataset.theme = themes.dark
  return webDarkTheme
}
const saveTheme = (theme) => {
  document.documentElement.dataset.theme = theme
  localStorage.setItem('theme', `${theme}`)
}

const ThemeProvider = ({ children }) => {
  const [theme, setTheme] = React.useState(getTheme)

  return (
    <FluentProvider theme={theme}>
      {children}
    </FluentProvider>
  )
}

export default ThemeProvider
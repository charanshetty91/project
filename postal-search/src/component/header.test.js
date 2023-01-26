import { render, screen } from '@testing-library/react';
import Header from './header';

test('renders Postal code Search', () => {
    render(<Header />);
    const linkElement = screen.getByText(/Postal code Search:/i);
    expect(linkElement).toBeInTheDocument();
  });